using FluentAssertions;
using InvestmentMngmt.Application.Investment.Commands.Get;
using InvestmentMngmt.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentMngmt.Tests.Application.Investment.Commands.Get
{
    public class HandlerTest: BaseTest
    {
        private readonly Mock<IInvestmentSummaryService> _investmentSummaryService;
        private readonly Mock<ITracer> _tracer;
        private readonly Mock<IDistributedCache> _distributedCache;

        private readonly Handler _handler;

        public HandlerTest()
        {
            _investmentSummaryService = new Mock<IInvestmentSummaryService>();
            _tracer = new Mock<ITracer>();            
            _distributedCache = new Mock<IDistributedCache>();

            _handler = new Handler(_investmentSummaryService.Object, _distributedCache.Object, _tracer.Object);
        }

        [Fact]
        public async Task ShouldBeReturnResultWithoutUsingCache()
        {
            _distributedCache.Setup(x => x.GetAsync(DistributedCacheKey, CancellationToken.None)).ReturnsAsync(() => null);

            _tracer.Setup(x => x.ActiveSpan.SetTag(It.IsAny<string>(), false));

            _investmentSummaryService.Setup(x => x.GetSummaryAsync()).ReturnsAsync(InvestmentsResult);

            var resultResponseBytes = JsonSerializer.SerializeToUtf8Bytes(InvestmentsResult);

            _distributedCache.Setup(x => x.SetAsync(DistributedCacheKey, resultResponseBytes, It.IsAny<DistributedCacheEntryOptions>(), CancellationToken.None));

            var response = await _handler.Handle(new Request(), CancellationToken.None);

            _distributedCache.Verify(x => x.GetAsync(DistributedCacheKey, CancellationToken.None));
            _tracer.Verify(x => x.ActiveSpan.SetTag(It.IsAny<string>(), false));
            _investmentSummaryService.Verify(x => x.GetSummaryAsync());
            _distributedCache.Verify(x => x.SetAsync(DistributedCacheKey, resultResponseBytes, It.IsAny<DistributedCacheEntryOptions>(), CancellationToken.None));

            response.Data.Should().NotBeNull();
            response.IsValid.Should().BeTrue();
            response.Data.Should().BeOfType<Result>();
            response.Data.Should().BeEquivalentTo(InvestmentsResult);
        }

        [Fact]
        public async Task ShouldBeReturnResultUsingCache()
        {
            var resultResponseBytes = JsonSerializer.SerializeToUtf8Bytes(InvestmentsResult);

            _distributedCache.Setup(x => x.GetAsync(DistributedCacheKey, CancellationToken.None)).ReturnsAsync(() => resultResponseBytes);

            _tracer.Setup(x => x.ActiveSpan.SetTag(It.IsAny<string>(), true));

            var resultResponse = JsonSerializer.Deserialize<Result>(new ReadOnlySpan<byte>(resultResponseBytes));

            var response = await _handler.Handle(new Request(), CancellationToken.None);

            _distributedCache.Verify(x => x.GetAsync(DistributedCacheKey, CancellationToken.None));
            _tracer.Verify(x => x.ActiveSpan.SetTag(It.IsAny<string>(), true));                        

            response.Data.Should().NotBeNull();
            response.IsValid.Should().BeTrue();
            response.Data.Should().BeOfType<Result>();
            response.Data.Should().BeEquivalentTo(InvestmentsResult);
        }
    }
}
