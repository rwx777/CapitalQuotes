using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalQuotes.SKAPI
{
    partial class CapitalConnection
    {
        private Task QuotesOnConnection(int nKind, int nCode)
        {
            return Task.Run(() => {
                _logger.Info($"[CapitalConnection.QuotesOnConnection()] Kind: {nKind}");
                if (nKind == 3003)
                {
                    ConnectionReadyEvent?.Invoke();
                }
            });
        }

        private void _skQuotes_OnNotifyTicks(short sMarketNo, short sIndex, int nPtr, int nDate, int nTimehms, int nTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
        {
            _logger.Trace($"[RT()] {sMarketNo}, {sIndex}, {nTimehms}, {nTimemillismicros}, {nClose}");
        }

        private void _skQuotes_OnNotifyHistoryTicks(short sMarketNo, short sStockIdx, int nPtr, int nDate, int nTimehms, int nTimemillismicros, int nBid, int nAsk, int nClose, int nQty, int nSimulate)
        {
            //_logger.Trace($"[History()] {sMarketNo}, {sStockIdx}, {nTimehms}, {nTimemillismicros}, {nClose}");
        }
    }
}
