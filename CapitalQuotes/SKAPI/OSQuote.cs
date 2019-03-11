using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapitalQuotes.SKAPI
{
    partial class CapitalConnection
    {
        private void _skOSQuotes_OnNotifyTicks(short sStockIdx, int nPtr, int nDate, int nTime, int nClose, int nQty)
        {
            _logger.Trace($"[RTOS()] {sStockIdx}, {nDate}, {nTime}, {nClose}, {nQty}");
        }

        private Task OSQuotesOnConnection(int nKind, int nCode)
        {
            return Task.Run(() => {
                _logger.Info($"[CapitalConnection.OSQuotesOnConnection()] Kind: {nKind}");
                if (nKind == 3001)
                {
                    OSConnectionReadyEvent?.Invoke();
                }
            });
        }
    }
}
