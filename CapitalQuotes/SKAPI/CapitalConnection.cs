using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SKCOMLib;

namespace CapitalQuotes.SKAPI
{
    partial class CapitalConnection
    {
        Logger _logger;
        SKCenterLib _skCenter;
        SKQuoteLib _skQuotes;
        SKReplyLib _skReply;
        SKOSQuoteLib _skOSQuotes;

        #region CustomDefinedEvent
        public event Action ConnectionReadyEvent;
        public event Action OSConnectionReadyEvent;
        #endregion

        #region Constructor
        public CapitalConnection(Logger logger)
        {
            #region Dependency
            _logger = logger;
            _logger.Info("[CapitalConnection()] Begin of constructor...");
            _skCenter = new SKCenterLib();
            _skQuotes = new SKQuoteLib();
            _skReply = new SKReplyLib();
            _skOSQuotes = new SKOSQuoteLib();
            #endregion

            #region Event
            _skReply.OnConnect += async (a, b) => await ReplyOnConnect(a, b);

            _skQuotes.OnConnection += async (a, b) => await QuotesOnConnection(a, b);
            _skQuotes.OnNotifyHistoryTicks += _skQuotes_OnNotifyHistoryTicks;
            _skQuotes.OnNotifyTicks += _skQuotes_OnNotifyTicks;

            _skOSQuotes.OnConnect += async (a, b) => await OSQuotesOnConnection(a, b);
            _skOSQuotes.OnNotifyTicks += _skOSQuotes_OnNotifyTicks;

            ConnectionReadyEvent += async () => await ConnectionReady();
            OSConnectionReadyEvent += async () => await OSConnectionReady();
            #endregion
        }
        #endregion

        
        #region Event Method
        private Task OSConnectionReady()
        {
            return Task.Run(() => {
                _logger.Info("[CapitalConnection.OSConnectionReady()] OSConnection is ready.");
                _logger.Info("[CapitalConnection.OSConnectionReady()] Subscribe...");
                // _skOSQuotes.SKOSQuoteLib_RequestTicks(ref a, "HKEx,HSI1903");
                //_skOSQuotes.SKOSQuoteLib_RequestTicks(ref a, "CBOT,YM1903");
            });
        }

        private Task ConnectionReady()
        {
            return Task.Run(() => {
                _logger.Info("[CapitalConnection.ConnectionReady()] Connection is ready.");
                _logger.Info("[CapitalConnection.ConnectionReady()] Subscribe...");
                _skQuotes.SKQuoteLib_RequestTicks(-1, "TX00AM");
            });
        }
        #endregion

        private Task ReplyOnConnect(string bstrUserID, int nErrorCode)
        {
            return Task.Run(() => {
                _logger.Info($"[CapitalConnection.ReplyOnConnect()] {bstrUserID}, {nErrorCode}");
            });
        }

        public Task<int> Logon(string id,string pwd)
        {
            return Task.Run(()=>{
                _logger.Info("[CapitalConnection.Logon()] Login...");
                int returnCode = _skCenter.SKCenterLib_Login(id, pwd);
                //_skReply.SKReplyLib_ConnectByID(id);
                return returnCode;
            });
        }


        #region Method
        public Task<int> ConnectToQuoteServer()
        {
            return Task.Run(() => {
                _logger.Info("[CapitalConnection.ConnectToQuoteServer()] EnterMonitor...");
                var code1 = _skOSQuotes.SKOSQuoteLib_EnterMonitor();
                var code2 = _skOSQuotes.SKOSQuoteLib_SetOSQuoteServer(0);
                var code3 = _skQuotes.SKQuoteLib_EnterMonitor();
                _logger.Info($"[CapitalConnection.ConnectToOSQuoteServer()] EnterOSMonitor...{code1}, {code2}");
                _logger.Info($"[CapitalConnection.ConnectToQuoteServer()] EnterMonitor...{code3}");
                return 0;
            });
        }
        #endregion
    }
}
