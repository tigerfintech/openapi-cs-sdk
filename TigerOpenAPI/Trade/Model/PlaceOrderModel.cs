using System;
using System.Security.Principal;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Model;
using TigerOpenAPI.Trade.Response;

namespace TigerOpenAPI.Trade.Model
{
  public class PlaceOrderModel : TradeModel
  {
    public const string WAP_START_TIME = "start_time";
    public const string WAP_END_TIME = "end_time";
    public const string WAP_PARTICIPATION_RATE = "participation_rate";
    /**
     * order ID(the incremental value of the corresponding account)
     */
    [JsonProperty(PropertyName = "order_id")]
    public Int32 OrderId { get; set; }

    [JsonProperty(PropertyName = "sec_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public SecType SecType { get; set; }

    [JsonProperty(PropertyName = "market"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Market Market { get; set; }

    [JsonProperty(PropertyName = "currency"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public Currency Currency { get; set; }

    [JsonProperty(PropertyName = "symbol")]
    public string Symbol { get; set; }

    [JsonProperty(PropertyName = "right")]
    public string? Right { get; set; }

    [JsonProperty(PropertyName = "strike")]
    public string? Strike { get; set; }

    [JsonProperty(PropertyName = "expiry")]
    public string? Expiry { get; set; }

    [JsonProperty(PropertyName = "action"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ActionType Action { get; set; }

    [JsonProperty(PropertyName = "order_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public OrderType OrderType { get; set; }

    [JsonProperty(PropertyName = "total_quantity")]
    public Int64? TotalQuantity { get; set; }

    [JsonProperty(PropertyName = "limit_price")]
    public Double? LimitPrice { get; set; }

    [JsonProperty(PropertyName = "cash_amount")]
    public Double? CashAmount { get; set; }

    /**
     * 价格微调幅度（默认为0表示不调整，正数为向上调整，负数向下调整），对传入价格自动调整到合法价位上
     * 例如：0.001 代表向上调整且幅度不超过 0.1%；-0.001 代表向下调整且幅度不超过 0.1%。默认 0 表示不调整
     */
    [JsonProperty(PropertyName = "adjust_limit")]
    public Double AdjustLimit { get; set; }


    /**
     * stop loss price. This parameter is required when order_type is STP and STP_LMT,
     * when order_type is TRAIL, aux_price is the stop loss trailing amount
     */
    [JsonProperty(PropertyName = "aux_price")]
    public Double? AuxPrice { get; set; }

    /**
     * Trailing Stop Order - trailing percentage. When order_type is TRAIL,
     * trailing_percent is preferred when both aux_price and trailing_percent have values
     */
    [JsonProperty(PropertyName = "trailing_percent")]
    public Double? TrailingPercent { get; set; }

    /**
     * order validity time range
     * DAY：valid for the day
     * GTC：valid until cancelled
     * GTD：valid until the specified date
     * AUC：call auction（ HK stock，order type is 'MTL'，required limitPrice）
     */
    [JsonProperty(PropertyName = "time_in_force"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public TimeInForce TimeInForce { get; set; } = TimeInForce.DAY;

    /**
     * GTD order's expire time
     */
    [JsonProperty(PropertyName = "expire_time")]
    public long ExpireTime { get; set; }

    /**
     * Allow pre-market and after-hours trading. default is true
     */
    [JsonProperty(PropertyName = "outside_rth")]
    public Boolean OutsideRth { get; set; } = true;

    [JsonProperty(PropertyName = "exchange")]
    public string Exchange { get; set; }

    /**
     * lot size(for futures, options, warran, cbbc)
     */
    [JsonProperty(PropertyName = "multiplier")]
    public double Multiplier { get; set; }

    [JsonProperty(PropertyName = "local_symbol")]
    public string LocalSymbol { get; set; }

    [JsonProperty(PropertyName = "alloc_accounts")]
    public List<string> AllocAccounts { get; set; }
    [JsonProperty(PropertyName = "alloc_shares")]
    public List<Double> AllocShares { get; set; }

    [JsonProperty(PropertyName = "algo_strategy")]
    public string AlgoStrategy { get; set; }
    [JsonProperty(PropertyName = "algo_params")]
    public List<TagValue> AlgoParams { get; set; }

    /**
     * user remark info
     */
    [JsonProperty(PropertyName = "user_mark")]
    public string UserMark { get; set; }

    /**
     * attached order type：PROFIT/LOSS/BRACKETS
     */
    [JsonProperty(PropertyName = "attach_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public AttachType AttachType { get; set; }

    /**
     * attached profit taker order
     */
    [JsonProperty(PropertyName = "profit_taker_orderId")]
    public Int32 ProfitTakerOrderId { get; set; }
    [JsonProperty(PropertyName = "profit_taker_price")]
    public Double ProfitTakerPrice { get; set; }
    [JsonProperty(PropertyName = "profit_taker_tif"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public TimeInForce ProfitTakerTif { get; set; }
    [JsonProperty(PropertyName = "profit_taker_rth")]
    public Boolean ProfitTakerRth { get; set; }

    /**
     * attached stop loss order
     */
    [JsonProperty(PropertyName = "stop_loss_order_type"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public OrderType StopLossOrderType { get; set; }
    [JsonProperty(PropertyName = "stop_loss_orderId")]
    public Int32 StopLossOrderId { get; set; }
    [JsonProperty(PropertyName = "stop_loss_price")]
    public Double StopLossPrice { get; set; }
    [JsonProperty(PropertyName = "stop_loss_limit_price")]
    public Double StopLossLimitPrice { get; set; }
    [JsonProperty(PropertyName = "stop_loss_tif"), Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public TimeInForce StopLossTif { get; set; }

    /**
     * attached trailing stop loss order's trailing percent
     */
    [JsonProperty(PropertyName = "stop_loss_trailing_percent")]
    public Double StopLossTrailingPercent { get; set; }
    /**
     * attached trailing stop loss order's trailing amount
     */
    [JsonProperty(PropertyName = "stop_loss_trailing_amount")]
    public Double StopLossTrailingAmount { get; set; }

    /**
     * Multi Order's type: COVERED,PROTECTIVE,VERTICAL,STRADDLE,STRANGLE,CALENDAR,DIAGONAL,SYNTHETIC,CUSTOM
     */
    [JsonProperty(PropertyName = "combo_type")]
    public string ComboType { get; set; }
    [JsonProperty(PropertyName = "contract_legs")]
    private List<ContractLeg> ContractLegs { get; set; }

    /**
     * OCA BRACKETS order
     */
    [JsonProperty(PropertyName = "oca_orders")]
    private List<PlaceOrderModel> OcaOrders { get; set; }

    public PlaceOrderModel() : base()
    {
    }

    public void AddAlgoParam(TagValue? algoParam)
    {
      if (algoParam == null)
      {
        return;
      }
      if (this.AlgoParams == null)
      {
        this.AlgoParams = new List<TagValue>();
      }
      this.AlgoParams.Add(algoParam);
    }

    public static PlaceOrderModel BuildMarketOrder(string account, ContractItem contract,
        ActionType action, Int64 quantity)
    {
      PlaceOrderModel tradeOrderModel = BuildTradeOrderModel(account, contract, action, quantity);
      tradeOrderModel.OrderType = OrderType.MKT;
      return tradeOrderModel;
    }

    public static PlaceOrderModel BuildAmountOrder(string account, ContractItem contract,
        ActionType action, Double cashAmount)
    {
      PlaceOrderModel tradeOrderModel = BuildTradeOrderModel(account, contract, action, null);
      tradeOrderModel.OrderType = OrderType.MKT;
      tradeOrderModel.CashAmount = cashAmount;
      return tradeOrderModel;
    }

    public static PlaceOrderModel BuildLimitOrder(string account, ContractItem contract,
        ActionType action, Int64 quantity, Double limitPrice, Double adjustLimit = 0)
    {
      PlaceOrderModel tradeOrderModel = BuildTradeOrderModel(account, contract, action, quantity);
      tradeOrderModel.OrderType = OrderType.LMT;
      tradeOrderModel.LimitPrice = limitPrice;
      tradeOrderModel.AdjustLimit = adjustLimit;
      return tradeOrderModel;
    }

    public static PlaceOrderModel BuildAuctionOrder(string account, ContractItem contract,
        ActionType action, Int64 quantity, Double limitPrice,
        OrderType orderType = OrderType.AL,
        TimeInForce timeInForce = TimeInForce.OPG, Double adjustLimit = 0)
    {
      PlaceOrderModel tradeOrderModel = BuildTradeOrderModel(account, contract, action, quantity);
      tradeOrderModel.OrderType = orderType;
      tradeOrderModel.TimeInForce = timeInForce;
      tradeOrderModel.LimitPrice = limitPrice;
      tradeOrderModel.AdjustLimit = adjustLimit;
      return tradeOrderModel;
    }

    public static PlaceOrderModel BuildStopOrder(string account, ContractItem contract,
        ActionType action, Int64 quantity, Double auxPrice, Double adjustLimit = 0)
    {
      PlaceOrderModel tradeOrderModel = BuildTradeOrderModel(account, contract, action, quantity);
      tradeOrderModel.OrderType = OrderType.STP;
      tradeOrderModel.AuxPrice = auxPrice;
      tradeOrderModel.AdjustLimit = adjustLimit;
      return tradeOrderModel;
    }

    public static PlaceOrderModel BuildStopLimitOrder(string account, ContractItem contract,
        ActionType action, Int64 quantity, Double limitPrice, Double auxPrice, Double adjustLimit = 0)
    {
      PlaceOrderModel tradeOrderModel = BuildTradeOrderModel(account, contract, action, quantity);
      tradeOrderModel.OrderType = OrderType.STP_LMT;
      tradeOrderModel.LimitPrice = limitPrice;
      tradeOrderModel.AuxPrice = auxPrice;
      tradeOrderModel.AdjustLimit = adjustLimit;
      return tradeOrderModel;
    }

    public static PlaceOrderModel BuildTrailOrder(string account, ContractItem contract,
        ActionType action, Int64 quantity, Double trailingPercent, Double auxPrice)
    {
      PlaceOrderModel tradeOrderModel = BuildTradeOrderModel(account, contract, action, quantity);
      tradeOrderModel.OrderType = OrderType.TRAIL;
      tradeOrderModel.TrailingPercent = trailingPercent;
      tradeOrderModel.AuxPrice = auxPrice;
      return tradeOrderModel;
    }

    public static PlaceOrderModel BuildTradeOrderModel(string account, ContractItem contract,
      ActionType action, Int64? quantity)
    {
      if (contract == null)
      {
        throw new ArgumentNullException("parameter 'contract' is null");
      }

      PlaceOrderModel model = new PlaceOrderModel();
      model.Account = account;
      model.Action = action;
      model.TotalQuantity = quantity;
      model.Symbol = contract.Symbol;
      model.Currency = contract.Currency == null
        ? Currency.NONE : (Currency)Enum.Parse(typeof(Currency), contract.Currency, true);
      model.SecType = contract.SecType == null
        ? SecType.NONE : (SecType)Enum.Parse(typeof(SecType), contract.SecType, true);
      model.Exchange = contract.Exchange;
      model.Market = contract.Market == null
        ? Market.NONE : (Market)Enum.Parse(typeof(Market), contract.Market, true);
      model.LocalSymbol = contract.LocalSymbol;
      model.Expiry = contract.Expiry;
      model.Strike = contract.Strike.CompareTo(0) <= 0
        ? null : contract.Strike.ToString();
      model.Right = contract.Right;
      model.Multiplier = contract.Multiplier;
      if (model.SecType == SecType.FUT)
      {
        if (AccountUtil.IsGlobalAccount(account))
        {
          if (!string.IsNullOrWhiteSpace(contract.Type))
          {
            model.Symbol= contract.Type;
          }
          if (string.IsNullOrWhiteSpace(model.Expiry)
            && !string.IsNullOrWhiteSpace(contract.LastTradingDate))
          {
            model.Expiry = contract.LastTradingDate;
          }
        }
        else
        {
          model.Expiry = null;
        }
      }
      return model;
    }

    public static PlaceOrderModel BuildMultiLegOrder(string account,
        List<ContractLeg> contractLegs, ComboType comboType, ActionType action, Int32 quantity,
        OrderType orderType, Double? limitPrice, Double? auxPrice, Double? trailingPercent)
    {
      if (contractLegs is null)
      {
        throw new ArgumentException("parameter 'contractLegs' is null");
      }
      if (orderType == OrderType.NONE)
      {
        throw new ArgumentException("parameter 'orderType' is NONE");
      }
      PlaceOrderModel model = new PlaceOrderModel();
      model.SecType = SecType.MLEG;
      model.ComboType = comboType.ToString();
      model.Account = account;
      model.Action = action;
      model.TotalQuantity = quantity;
      model.ContractLegs = contractLegs;

      model.OrderType = orderType;
      model.LimitPrice = limitPrice;
      model.AuxPrice = auxPrice;
      model.TrailingPercent = trailingPercent;
      model.TimeInForce = TimeInForce.DAY;
      return model;
    }

    public static PlaceOrderModel BuildTWAPOrder(string account,
        string symbol, ActionType action, Int32 quantity,
        Int64? startTime, Int64? endTime, Double? limitPrice)
    {
      return BuildWAPOrder(account, symbol, action, quantity, OrderType.TWAP,
          startTime, endTime, null, limitPrice);
    }

    public static PlaceOrderModel BuildVWAPOrder(string account,
        string symbol, ActionType action, Int32 quantity,
        Int64? startTime, Int64? endTime,
        Double? participationRate, Double? limitPrice)
    {
      return BuildWAPOrder(account, symbol, action, quantity, OrderType.VWAP,
          startTime, endTime, participationRate, limitPrice);
    }

    public static PlaceOrderModel BuildWAPOrder(string account,
        string symbol, ActionType action, Int32 quantity,
        OrderType orderType, Int64? startTime, Int64? endTime,
        Double? participationRate,
        Double? limitPrice)
    {
      if (OrderType.TWAP != orderType && OrderType.VWAP != orderType)
      {
        throw new ArgumentException("parameter 'orderType' must be ['TWAP', 'VWAP']");
      }

      PlaceOrderModel model = new PlaceOrderModel();
      model.OutsideRth = false;
      model.SecType = SecType.STK;
      model.Account = account;
      model.Action = action;
      model.TotalQuantity = quantity;
      model.Symbol = symbol;
      model.OrderType = orderType;
      model.LimitPrice = limitPrice;
      model.TimeInForce = TimeInForce.DAY;

      model.AlgoStrategy = orderType.ToString();
      model.AddAlgoParam(TagValue.BuildTagValue(WAP_START_TIME, startTime));
      model.AddAlgoParam(TagValue.BuildTagValue(WAP_END_TIME, endTime));
      if (OrderType.VWAP == orderType)
      {
        model.AddAlgoParam(TagValue.BuildTagValue(WAP_PARTICIPATION_RATE, participationRate));
      }
      return model;
    }

    public static PlaceOrderModel BuildOCABracketsOrder(
      string account, ContractItem contract, ActionType action, Int64 quantity,
      Double profitTakerPrice, TimeInForce profitTakerTif, Boolean profitTakerRth,
      Double stopLossPrice, TimeInForce stopLossTif, Boolean stopLossRth, Double? stopLossLimitPrice = null)
    {
      PlaceOrderModel profitTakerOrder = BuildTradeOrderModel(account, contract, action, quantity);
      profitTakerOrder.OrderType = OrderType.LMT;
      profitTakerOrder.LimitPrice = profitTakerPrice;
      profitTakerOrder.TimeInForce = profitTakerTif;
      profitTakerOrder.OutsideRth = profitTakerRth;

      PlaceOrderModel stopLossOrder = BuildTradeOrderModel(account, contract, action, quantity);
      if (stopLossLimitPrice == null)
      {
        stopLossOrder.OrderType = OrderType.STP;
      }
      else
      {
        stopLossOrder.OrderType = OrderType.STP_LMT;
        stopLossOrder.LimitPrice = stopLossLimitPrice;
      }
      stopLossOrder.AuxPrice = stopLossPrice;
      stopLossOrder.TimeInForce = stopLossTif;
      stopLossOrder.OutsideRth = stopLossRth;

      PlaceOrderModel tradeOrderModel = new PlaceOrderModel()
      {
        OcaOrders = new List<PlaceOrderModel>() { profitTakerOrder, stopLossOrder },
      };
      return tradeOrderModel;
    }

    public PlaceOrderModel AddProfitTakerOrder(
        Double profitTakerPrice, TimeInForce profitTakerTif, Boolean profitTakerRth)
    {
      AttachType = AttachType.PROFIT;
      ProfitTakerPrice = profitTakerPrice;
      ProfitTakerTif = profitTakerTif;
      ProfitTakerRth = profitTakerRth;
      return this;
    }

    public PlaceOrderModel AddStopLossOrder(
        Double stopLossPrice, TimeInForce stopLossTif)
    {
      AttachType = AttachType.LOSS;
      StopLossOrderType = OrderType.STP;
      StopLossPrice = stopLossPrice;
      StopLossTif = stopLossTif;
      return this;
    }

    public PlaceOrderModel AddStopLossLimitOrder(
        Double stopLossPrice, Double stopLossLimitPrice, TimeInForce stopLossTif)
    {
      AttachType = AttachType.LOSS;
      StopLossOrderType = OrderType.STP_LMT;
      StopLossPrice = stopLossPrice;
      StopLossLimitPrice = stopLossLimitPrice;
      StopLossTif = stopLossTif;
      return this;
    }

    public PlaceOrderModel AddStopLossTrailOrder(
        Double stopLossTrailingPercent, Double stopLossTrailingAmount, TimeInForce stopLossTif)
    {
      AttachType = AttachType.LOSS;
      StopLossOrderType = OrderType.TRAIL;
      StopLossTrailingPercent = stopLossTrailingPercent;
      StopLossTrailingAmount = stopLossTrailingAmount;
      StopLossTif = stopLossTif;
      return this;
    }

    public PlaceOrderModel AddBracketsOrder(
        Double profitTakerPrice, TimeInForce profitTakerTif, Boolean profitTakerRth,
        Double stopLossPrice, TimeInForce stopLossTif, Double stopLossLimitPrice = default)
    {
      AttachType = AttachType.BRACKETS;
      ProfitTakerPrice = profitTakerPrice;
      ProfitTakerTif = profitTakerTif;
      ProfitTakerRth = profitTakerRth;
      StopLossPrice = stopLossPrice;
      StopLossTif = stopLossTif;
      StopLossLimitPrice = stopLossLimitPrice;
      return this;
    }
  }
}

