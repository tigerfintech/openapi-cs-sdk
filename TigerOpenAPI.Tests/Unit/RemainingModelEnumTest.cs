using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NUnit.Framework;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Quote.Model;
using TigerOpenAPI.Trade.Model;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Tests for the remaining Trade/Quote model classes and enum-like classes
  /// that are not covered by <see cref="TradeModelTest"/>, <see cref="QuoteModelTest"/>,
  /// <see cref="EnumValuesTest"/> or <see cref="EnumWireValueTest"/>.
  /// Zero network — pure model construction, JSON wire-name assertions and enum behavior.
  /// </summary>
  [TestFixture]
  public class RemainingModelEnumTest
  {
    // ================================================================
    //  Trade models
    // ================================================================

    // ---- FundDetailsModel ----

    [Test]
    public void FundDetailsModel_DefaultConstructor_AllPropertiesNull()
    {
      var model = new FundDetailsModel();
      Assert.That(model.SegTypes, Is.Null);
      Assert.That(model.FundType, Is.Null);
      Assert.That(model.Currency, Is.Null);
      Assert.That(model.StartDate, Is.Null);
      Assert.That(model.EndDate, Is.Null);
      Assert.That(model.Start, Is.Null);
      Assert.That(model.Limit, Is.Null);
      Assert.That(model.Account, Is.Null);
    }

    [Test]
    public void FundDetailsModel_AccountConstructor_SetsAccount()
    {
      var model = new FundDetailsModel("U12345");
      Assert.That(model.Account, Is.EqualTo("U12345"));
      Assert.That(model.SegTypes, Is.Null);
    }

    [Test]
    public void FundDetailsModel_AccountSegTypesConstructor_SetsBoth()
    {
      var segTypes = new List<string> { "SEC", "FUT" };
      var model = new FundDetailsModel("U12345", segTypes);
      Assert.That(model.Account, Is.EqualTo("U12345"));
      Assert.That(model.SegTypes, Is.EqualTo(segTypes));
    }

    [Test]
    public void FundDetailsModel_AccountSegTypesSecretKeyConstructor_SetsSecretKey()
    {
      var model = new FundDetailsModel("U12345", new List<string> { "SEC" }, "sk-secret");
      Assert.That(model.SecretKey, Is.EqualTo("sk-secret"));
    }

    [Test]
    public void FundDetailsModel_FullConstructor_SetsFundType()
    {
      var model = new FundDetailsModel("U12345", new List<string> { "SEC" }, "FUND_TYPE_A", "sk-secret");
      Assert.That(model.FundType, Is.EqualTo("FUND_TYPE_A"));
    }

    [Test]
    public void FundDetailsModel_StartLimitConstructor_SetsPaging()
    {
      var model = new FundDetailsModel("U12345", new List<string> { "SEC" }, 100L, 50L);
      Assert.That(model.Start, Is.EqualTo(100L));
      Assert.That(model.Limit, Is.EqualTo(50L));
    }

    [Test]
    public void FundDetailsModel_StartLimitSecretKeyConstructor_SetsAll()
    {
      var model = new FundDetailsModel("U12345", new List<string> { "SEC" }, 100L, 50L, "sk-secret");
      Assert.That(model.Start, Is.EqualTo(100L));
      Assert.That(model.Limit, Is.EqualTo(50L));
      Assert.That(model.SecretKey, Is.EqualTo("sk-secret"));
    }

    [Test]
    public void FundDetailsModel_Serialization_WireNamesPresent()
    {
      var model = new FundDetailsModel("U12345", new List<string> { "SEC", "FUT" }, "FUND_A", "sk-secret")
      {
        Currency = "USD",
        StartDate = "2024-01-01",
        EndDate = "2024-12-31",
        Start = 0L,
        Limit = 100L
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"seg_types\""));
      Assert.That(json, Does.Contain("\"fund_type\":\"FUND_A\""));
      Assert.That(json, Does.Contain("\"currency\":\"USD\""));
      Assert.That(json, Does.Contain("\"start_date\":\"2024-01-01\""));
      Assert.That(json, Does.Contain("\"end_date\":\"2024-12-31\""));
      Assert.That(json, Does.Contain("\"start\":0"));
      Assert.That(json, Does.Contain("\"limit\":100"));
    }

    // ---- PositionTransferModel ----

    [Test]
    public void PositionTransferModel_DefaultConstructor_PropertiesNull()
    {
      var model = new PositionTransferModel();
      Assert.That(model.FromAccount, Is.Null);
      Assert.That(model.ToAccount, Is.Null);
      Assert.That(model.Market, Is.Null);
      Assert.That(model.Transfers, Is.Null);
    }

    [Test]
    public void PositionTransferModel_FullConstructor_SetsAllFields()
    {
      var transfers = new List<PositionTransferDetail>
      {
        new PositionTransferDetail { Symbol = "AAPL", Quantity = 100 }
      };
      var model = new PositionTransferModel("U111", "U222", transfers, "US");
      Assert.That(model.FromAccount, Is.EqualTo("U111"));
      Assert.That(model.ToAccount, Is.EqualTo("U222"));
      Assert.That(model.Market, Is.EqualTo("US"));
      Assert.That(model.Transfers.Count, Is.EqualTo(1));
    }

    [Test]
    public void PositionTransferModel_Serialization_WireNamesPresent()
    {
      var transfers = new List<PositionTransferDetail>
      {
        new PositionTransferDetail
        {
          Symbol = "AAPL",
          SecType = "STK",
          Expiry = "20240119",
          Strike = "180",
          Right = "CALL",
          Quantity = 100
        }
      };
      var model = new PositionTransferModel("U111", "U222", transfers, "US");
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"from_account\":\"U111\""));
      Assert.That(json, Does.Contain("\"to_account\":\"U222\""));
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"transfers\""));
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"sec_type\":\"STK\""));
      Assert.That(json, Does.Contain("\"expiry\":\"20240119\""));
      Assert.That(json, Does.Contain("\"strike\":\"180\""));
      Assert.That(json, Does.Contain("\"right\":\"CALL\""));
      Assert.That(json, Does.Contain("\"quantity\":100"));
    }

    [Test]
    public void PositionTransferDetail_DefaultConstructor_PropertiesNullOrDefault()
    {
      var detail = new PositionTransferDetail();
      Assert.That(detail.Symbol, Is.Null);
      Assert.That(detail.SecType, Is.Null);
      Assert.That(detail.Expiry, Is.Null);
      Assert.That(detail.Strike, Is.Null);
      Assert.That(detail.Right, Is.Null);
      Assert.That(detail.Quantity, Is.EqualTo(0L));
    }

    // ---- PositionTransferRecordsModel ----

    [Test]
    public void PositionTransferRecordsModel_DefaultConstructor_PropertiesNull()
    {
      var model = new PositionTransferRecordsModel();
      Assert.That(model.AccountId, Is.Null);
      Assert.That(model.SinceDate, Is.Null);
      Assert.That(model.ToDate, Is.Null);
      Assert.That(model.Status, Is.Null);
      Assert.That(model.Market, Is.Null);
      Assert.That(model.Symbol, Is.Null);
    }

    [Test]
    public void PositionTransferRecordsModel_ThreeArgConstructor_SetsDates()
    {
      var model = new PositionTransferRecordsModel("ACC1", "2024-01-01", "2024-12-31");
      Assert.That(model.AccountId, Is.EqualTo("ACC1"));
      Assert.That(model.SinceDate, Is.EqualTo("2024-01-01"));
      Assert.That(model.ToDate, Is.EqualTo("2024-12-31"));
    }

    [Test]
    public void PositionTransferRecordsModel_SixArgConstructor_SetsAll()
    {
      var model = new PositionTransferRecordsModel("ACC1", "2024-01-01", "2024-12-31", "DONE", "US", "AAPL");
      Assert.That(model.Status, Is.EqualTo("DONE"));
      Assert.That(model.Market, Is.EqualTo("US"));
      Assert.That(model.Symbol, Is.EqualTo("AAPL"));
    }

    [Test]
    public void PositionTransferRecordsModel_Serialization_WireNamesPresent()
    {
      var model = new PositionTransferRecordsModel("ACC1", "2024-01-01", "2024-12-31", "DONE", "US", "AAPL");
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"account_id\":\"ACC1\""));
      Assert.That(json, Does.Contain("\"since_date\":\"2024-01-01\""));
      Assert.That(json, Does.Contain("\"to_date\":\"2024-12-31\""));
      Assert.That(json, Does.Contain("\"status\":\"DONE\""));
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
    }

    // ---- PositionTransferDetailModel ----

    [Test]
    public void PositionTransferDetailModel_DefaultConstructor_IdZero()
    {
      var model = new PositionTransferDetailModel();
      Assert.That(model.Id, Is.EqualTo(0L));
      Assert.That(model.AccountId, Is.Null);
    }

    [Test]
    public void PositionTransferDetailModel_TwoArgConstructor_SetsIdAndAccount()
    {
      var model = new PositionTransferDetailModel(999L, "ACC1");
      Assert.That(model.Id, Is.EqualTo(999L));
      Assert.That(model.AccountId, Is.EqualTo("ACC1"));
    }

    [Test]
    public void PositionTransferDetailModel_Serialization_WireNamesPresent()
    {
      var model = new PositionTransferDetailModel(999L, "ACC1");
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"id\":999"));
      Assert.That(json, Does.Contain("\"account_id\":\"ACC1\""));
    }

    // ---- AggregateAssetModel ----

    [Test]
    public void AggregateAssetModel_DefaultConstructor_PropertiesNull()
    {
      var model = new AggregateAssetModel();
      Assert.That(model.SegType, Is.Null);
      Assert.That(model.BaseCurrency, Is.Null);
      Assert.That(model.Account, Is.Null);
    }

    [Test]
    public void AggregateAssetModel_AccountSegTypeConstructor_SetsBoth()
    {
      var model = new AggregateAssetModel("U12345", "SEC");
      Assert.That(model.Account, Is.EqualTo("U12345"));
      Assert.That(model.SegType, Is.EqualTo("SEC"));
    }

    [Test]
    public void AggregateAssetModel_AccountSegTypeSecretKeyConstructor_SetsSecretKey()
    {
      var model = new AggregateAssetModel("U12345", "SEC", "sk-secret");
      Assert.That(model.SecretKey, Is.EqualTo("sk-secret"));
    }

    [Test]
    public void AggregateAssetModel_FullConstructor_SetsBaseCurrency()
    {
      var model = new AggregateAssetModel("U12345", "SEC", "USD", "sk-secret");
      Assert.That(model.BaseCurrency, Is.EqualTo("USD"));
    }

    [Test]
    public void AggregateAssetModel_Serialization_WireNamesPresent()
    {
      var model = new AggregateAssetModel("U12345", "SEC", "USD", "sk-secret");
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"seg_type\":\"SEC\""));
      Assert.That(json, Does.Contain("\"base_currency\":\"USD\""));
    }

    // ---- PrimeAssetsModel ----

    [Test]
    public void PrimeAssetsModel_DefaultConstructor_ConsolidatedFalse()
    {
      var model = new PrimeAssetsModel();
      Assert.That(model.Consolidated, Is.False);
      Assert.That(model.BaseCurrency, Is.Null);
    }

    [Test]
    public void PrimeAssetsModel_Serialization_WireNamesPresent()
    {
      var model = new PrimeAssetsModel { BaseCurrency = "USD", Consolidated = true };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"base_currency\":\"USD\""));
      Assert.That(json, Does.Contain("\"consolidated\":true"));
    }

    // ---- PrimeAnalyticsAssetModel ----

    [Test]
    public void PrimeAnalyticsAssetModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new PrimeAnalyticsAssetModel();
      Assert.That(model.SegType, Is.EqualTo(SegmentType.NONE));
      Assert.That(model.Currency, Is.EqualTo(Currency.NONE));
      Assert.That(model.SubAccount, Is.Null);
      Assert.That(model.StartDate, Is.Null);
      Assert.That(model.EndDate, Is.Null);
    }

    [Test]
    public void PrimeAnalyticsAssetModel_Serialization_WireNamesPresent()
    {
      var model = new PrimeAnalyticsAssetModel
      {
        SegType = SegmentType.SEC,
        Currency = Currency.USD,
        SubAccount = "SUB1",
        StartDate = "2024-01-01",
        EndDate = "2024-12-31"
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"seg_type\":\"SEC\""));
      Assert.That(json, Does.Contain("\"currency\":\"USD\""));
      Assert.That(json, Does.Contain("\"sub_account\":\"SUB1\""));
      Assert.That(json, Does.Contain("\"start_date\":\"2024-01-01\""));
      Assert.That(json, Does.Contain("\"end_date\":\"2024-12-31\""));
    }

    // ================================================================
    //  Quote models
    // ================================================================

    // ---- WarrantFilterModel ----

    [Test]
    public void WarrantFilterModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new WarrantFilterModel();
      Assert.That(model.Symbol, Is.Null);
      Assert.That(model.Page, Is.EqualTo(0));
      Assert.That(model.PageSize, Is.EqualTo(0));
      Assert.That(model.SortFieldName, Is.Null);
      Assert.That(model.SortDir, Is.EqualTo(SortDir.SortDir_No));
      Assert.That(model.WarrantType, Is.Null);
      Assert.That(model.IssuerName, Is.Null);
      Assert.That(model.ExpireYM, Is.Null);
      Assert.That(model.State, Is.EqualTo(0));
      Assert.That(model.InOutPrice, Is.Null);
      Assert.That(model.LotSize, Is.Null);
      Assert.That(model.EntitlementRatio, Is.Null);
      Assert.That(model.Strike, Is.Null);
      Assert.That(model.EffectiveLeverage, Is.Null);
      Assert.That(model.LeverageRatio, Is.Null);
      Assert.That(model.CallPrice, Is.Null);
      Assert.That(model.Volume, Is.Null);
      Assert.That(model.Premium, Is.Null);
      Assert.That(model.OutstandingRatio, Is.Null);
      Assert.That(model.ImpliedVolatility, Is.Null);
    }

    [Test]
    public void WarrantFilterModel_Serialization_WireNamesPresent()
    {
      var model = new WarrantFilterModel
      {
        Symbol = "AAPL",
        Page = 1,
        PageSize = 20,
        SortFieldName = "volume",
        SortDir = SortDir.SortDir_Descend,
        WarrantType = new HashSet<Int32> { 1, 2 },
        IssuerName = "SG",
        ExpireYM = "2024-06",
        State = 1,
        InOutPrice = new HashSet<Int32> { -1, 1 },
        LotSize = new HashSet<Int32> { 100 },
        EntitlementRatio = new HashSet<Double> { 10.0 },
        Strike = new Range<Double>(100.0, 200.0),
        EffectiveLeverage = new Range<Double>(1.0, 5.0),
        LeverageRatio = new Range<Double>(2.0, 8.0),
        CallPrice = new Range<Double>(0.1, 0.5),
        Volume = new Range<Int64>(1000, 5000),
        Premium = new Range<Double>(0.05, 0.15),
        OutstandingRatio = new Range<Double>(0.1, 0.9),
        ImpliedVolatility = new Range<Double>(0.2, 0.8)
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"page\":1"));
      Assert.That(json, Does.Contain("\"page_size\":20"));
      Assert.That(json, Does.Contain("\"sort_field_name\":\"volume\""));
      Assert.That(json, Does.Contain("\"sort_dir\":\"SortDir_Descend\""));
      Assert.That(json, Does.Contain("\"warrant_type\""));
      Assert.That(json, Does.Contain("\"issuer_name\":\"SG\""));
      Assert.That(json, Does.Contain("\"expire_ym\":\"2024-06\""));
      Assert.That(json, Does.Contain("\"state\":1"));
      Assert.That(json, Does.Contain("\"in_out_price\""));
      Assert.That(json, Does.Contain("\"lot_size\""));
      Assert.That(json, Does.Contain("\"entitlement_ratio\""));
      Assert.That(json, Does.Contain("\"strike\""));
      Assert.That(json, Does.Contain("\"effective_leverage\""));
      Assert.That(json, Does.Contain("\"leverage_ratio\""));
      Assert.That(json, Does.Contain("\"call_price\""));
      Assert.That(json, Does.Contain("\"volume\""));
      Assert.That(json, Does.Contain("\"premium\""));
      Assert.That(json, Does.Contain("\"outstanding_ratio\""));
      Assert.That(json, Does.Contain("\"implied_volatility\""));
    }

    // ---- WarrantQuoteModel ----

    [Test]
    public void WarrantQuoteModel_DefaultConstructor_SymbolsNull()
    {
      var model = new WarrantQuoteModel();
      Assert.That(model.Symbols, Is.Null);
    }

    [Test]
    public void WarrantQuoteModel_Serialization_WireNamesPresent()
    {
      var model = new WarrantQuoteModel { Symbols = new List<string> { "10562.HK", "10563.HK" } };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbols\""));
      Assert.That(json, Does.Contain("10562.HK"));
      Assert.That(json, Does.Contain("10563.HK"));
    }

    // ---- CorporateActionModel ----

    [Test]
    public void CorporateActionModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new CorporateActionModel();
      Assert.That(model.Market, Is.EqualTo(Market.NONE));
      Assert.That(model.Symbols, Is.Null);
      Assert.That(model.ActionType, Is.EqualTo(CorporateActionType.NONE));
      Assert.That(model.BeginDate, Is.EqualTo(0L));
      Assert.That(model.EndDate, Is.EqualTo(0L));
    }

    [Test]
    public void CorporateActionModel_Serialization_WireNamesPresent()
    {
      var model = new CorporateActionModel
      {
        Market = Market.US,
        Symbols = new List<string> { "AAPL" },
        ActionType = CorporateActionType.DIVIDEND,
        BeginDate = 20240101L,
        EndDate = 20241231L
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"symbols\""));
      Assert.That(json, Does.Contain("\"action_type\":\"DIVIDEND\""));
      Assert.That(json, Does.Contain("\"begin_date\":20240101"));
      Assert.That(json, Does.Contain("\"end_date\":20241231"));
    }

    // ---- FinancialCurrencyModel ----

    [Test]
    public void FinancialCurrencyModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new FinancialCurrencyModel();
      Assert.That(model.Market, Is.EqualTo(Market.NONE));
      Assert.That(model.Symbols, Is.Null);
    }

    [Test]
    public void FinancialCurrencyModel_Serialization_WireNamesPresent()
    {
      var model = new FinancialCurrencyModel
      {
        Market = Market.HK,
        Symbols = new List<string> { "00700.HK" }
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"HK\""));
      Assert.That(json, Does.Contain("\"symbols\""));
    }

    // ---- FinancialExchangeRateModel ----

    [Test]
    public void FinancialExchangeRateModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new FinancialExchangeRateModel();
      Assert.That(model.CurrencyList, Is.Null);
      Assert.That(model.BeginDate, Is.EqualTo(0L));
      Assert.That(model.EndDate, Is.EqualTo(0L));
    }

    [Test]
    public void FinancialExchangeRateModel_Serialization_WireNamesPresent()
    {
      var model = new FinancialExchangeRateModel
      {
        CurrencyList = new List<string> { "USDCNH", "USDHKD" },
        BeginDate = 20240101L,
        EndDate = 20241231L
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"currency_list\""));
      Assert.That(json, Does.Contain("USDCNH"));
      Assert.That(json, Does.Contain("\"begin_date\":20240101"));
      Assert.That(json, Does.Contain("\"end_date\":20241231"));
    }

    // ---- FundSymbolModel ----

    [Test]
    public void FundSymbolModel_DefaultConstructor_SymbolsNull()
    {
      var model = new FundSymbolModel();
      Assert.That(model.Symbols, Is.Null);
    }

    [Test]
    public void FundSymbolModel_Serialization_WireNamesPresent()
    {
      var model = new FundSymbolModel { Symbols = new List<string> { "FU000001" } };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbols\""));
      Assert.That(json, Does.Contain("FU000001"));
    }

    // ---- FundQuoteHistoryModel ----

    [Test]
    public void FundQuoteHistoryModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new FundQuoteHistoryModel();
      Assert.That(model.Symbols, Is.Null);
      Assert.That(model.BeginTime, Is.EqualTo(0L));
      Assert.That(model.EndTime, Is.EqualTo(0L));
      Assert.That(model.Limit, Is.EqualTo(0));
    }

    [Test]
    public void FundQuoteHistoryModel_Serialization_WireNamesPresent()
    {
      var model = new FundQuoteHistoryModel
      {
        Symbols = new List<string> { "FU000001" },
        BeginTime = 1700000000L,
        EndTime = 1800000000L,
        Limit = 50
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbols\""));
      Assert.That(json, Does.Contain("\"begin_time\":1700000000"));
      Assert.That(json, Does.Contain("\"end_time\":1800000000"));
      Assert.That(json, Does.Contain("\"limit\":50"));
    }

    // ---- FutureContractByConCodeModel ----

    [Test]
    public void FutureContractByConCodeModel_DefaultConstructor_ContractCodeNull()
    {
      var model = new FutureContractByConCodeModel();
      Assert.That(model.ContractCode, Is.Null);
    }

    [Test]
    public void FutureContractByConCodeModel_Serialization_WireNamesPresent()
    {
      var model = new FutureContractByConCodeModel { ContractCode = "CL2401" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"contract_code\":\"CL2401\""));
    }

    // ---- FutureContractByExchCodeModel ----

    [Test]
    public void FutureContractByExchCodeModel_DefaultConstructor_ExchangeCodeNull()
    {
      var model = new FutureContractByExchCodeModel();
      Assert.That(model.ExchangeCode, Is.Null);
    }

    [Test]
    public void FutureContractByExchCodeModel_Serialization_WireNamesPresent()
    {
      var model = new FutureContractByExchCodeModel { ExchangeCode = "CME" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"exchange_code\":\"CME\""));
    }

    // ---- FutureContractByTypeModel ----

    [Test]
    public void FutureContractByTypeModel_DefaultConstructor_FutureTypeNull()
    {
      var model = new FutureContractByTypeModel();
      Assert.That(model.FutureType, Is.Null);
    }

    [Test]
    public void FutureContractByTypeModel_Serialization_WireNamesPresent()
    {
      var model = new FutureContractByTypeModel { FutureType = "FX" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"type\":\"FX\""));
    }

    // ---- FutureContractCodesModel ----

    [Test]
    public void FutureContractCodesModel_DefaultConstructor_ContractCodesNull()
    {
      var model = new FutureContractCodesModel();
      Assert.That(model.ContractCodes, Is.Null);
    }

    [Test]
    public void FutureContractCodesModel_Serialization_WireNamesPresent()
    {
      var model = new FutureContractCodesModel { ContractCodes = new List<string> { "CL2401", "ES2403" } };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"contract_codes\""));
      Assert.That(json, Does.Contain("CL2401"));
      Assert.That(json, Does.Contain("ES2403"));
    }

    // ---- FutureDepthModel ----

    [Test]
    public void FutureDepthModel_DefaultConstructor_ContractCodesNull()
    {
      var model = new FutureDepthModel();
      Assert.That(model.ContractCodes, Is.Null);
    }

    [Test]
    public void FutureDepthModel_ContractCodesConstructor_SetsContractCodes()
    {
      var codes = new List<string> { "CL2401" };
      var model = new FutureDepthModel(codes);
      Assert.That(model.ContractCodes, Is.EqualTo(codes));
    }

    // ---- FutureExchangeModel ----

    [Test]
    public void FutureExchangeModel_DefaultConstructor_SecTypeNull()
    {
      var model = new FutureExchangeModel();
      Assert.That(model.SecType, Is.Null);
    }

    [Test]
    public void FutureExchangeModel_Serialization_WireNamesPresent()
    {
      var model = new FutureExchangeModel { SecType = "FUT" };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"sec_type\":\"FUT\""));
    }

    // ---- FutureHistoryMainContractModel ----

    [Test]
    public void FutureHistoryMainContractModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new FutureHistoryMainContractModel();
      Assert.That(model.ContractCodes, Is.Null);
      Assert.That(model.BeginTime, Is.EqualTo(0L));
      Assert.That(model.EndTime, Is.EqualTo(0L));
    }

    [Test]
    public void FutureHistoryMainContractModel_Serialization_WireNamesPresent()
    {
      var model = new FutureHistoryMainContractModel
      {
        ContractCodes = new List<string> { "CL2401" },
        BeginTime = 1700000000L,
        EndTime = 1800000000L
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"contract_codes\""));
      Assert.That(json, Does.Contain("\"begin_time\":1700000000"));
      Assert.That(json, Does.Contain("\"end_time\":1800000000"));
    }

    // ---- FutureKlineModel ----

    [Test]
    public void FutureKlineModel_Defaults_PeriodMin_Limit300()
    {
      var model = new FutureKlineModel();
      Assert.That(model.Period, Is.EqualTo("min"));
      Assert.That(model.Limit, Is.EqualTo(300));
    }

    [Test]
    public void FutureKlineModel_Serialization_WireNamesPresent()
    {
      var model = new FutureKlineModel
      {
        ContractCodes = new List<string> { "CL2401" },
        Period = "day",
        BeginTime = 1700000000L,
        EndTime = 1800000000L,
        PageToken = "token123"
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"contract_codes\""));
      Assert.That(json, Does.Contain("\"period\":\"day\""));
      Assert.That(json, Does.Contain("\"begin_time\":1700000000"));
      Assert.That(json, Does.Contain("\"end_time\":1800000000"));
      Assert.That(json, Does.Contain("\"limit\":300"));
      Assert.That(json, Does.Contain("\"page_token\":\"token123\""));
    }

    // ---- FutureTickModel ----

    [Test]
    public void FutureTickModel_DefaultLimit_Is200()
    {
      var model = new FutureTickModel();
      Assert.That(model.Limit, Is.EqualTo(200));
      Assert.That(model.ContractCode, Is.Null);
      Assert.That(model.BeginIndex, Is.EqualTo(0L));
      Assert.That(model.EndIndex, Is.EqualTo(0L));
    }

    [Test]
    public void FutureTickModel_Serialization_WireNamesPresent()
    {
      var model = new FutureTickModel
      {
        ContractCode = "CL2401",
        BeginIndex = 10L,
        EndIndex = 100L,
        Limit = 50
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"contract_code\":\"CL2401\""));
      Assert.That(json, Does.Contain("\"begin_index\":10"));
      Assert.That(json, Does.Contain("\"end_index\":100"));
      Assert.That(json, Does.Contain("\"limit\":50"));
    }

    // ---- FutureTradingDateModel ----

    [Test]
    public void FutureTradingDateModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new FutureTradingDateModel();
      Assert.That(model.ContractCode, Is.Null);
      Assert.That(model.TradingDate, Is.EqualTo(0L));
    }

    [Test]
    public void FutureTradingDateModel_Serialization_WireNamesPresent()
    {
      var model = new FutureTradingDateModel { ContractCode = "CL2401", TradingDate = 20240115L };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"contract_code\":\"CL2401\""));
      Assert.That(json, Does.Contain("\"trading_date\":20240115"));
    }

    // ---- KlineQuotaModel ----

    [Test]
    public void KlineQuotaModel_DefaultConstructor_WithDetailsFalse()
    {
      var model = new KlineQuotaModel();
      Assert.That(model.WithDetails, Is.False);
    }

    [Test]
    public void KlineQuotaModel_Serialization_WireNamesPresent()
    {
      var model = new KlineQuotaModel { WithDetails = true };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"with_details\":true"));
    }

    // ---- MarketScannerModel ----

    [Test]
    public void MarketScannerModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new MarketScannerModel();
      Assert.That(model.Market, Is.EqualTo(Market.NONE));
      Assert.That(model.BaseFilterList, Is.Null);
      Assert.That(model.AccumulateFilterList, Is.Null);
      Assert.That(model.FinancialFilterList, Is.Null);
      Assert.That(model.MultiTagsFilterList, Is.Null);
      Assert.That(model.SortFieldData, Is.Null);
      Assert.That(model.Page, Is.EqualTo(0));
      Assert.That(model.PageSize, Is.EqualTo(0));
    }

    [Test]
    public void MarketScannerModel_Serialization_WireNamesPresent()
    {
      var model = new MarketScannerModel
      {
        Market = Market.US,
        BaseFilterList = new List<BaseFilter>
        {
          new BaseFilter { FieldName = StockField.StockField_CurPrice, FilterMin = 10.0, FilterMax = 20.0, IsNoFilter = false }
        },
        AccumulateFilterList = new List<AccumulateFilter>
        {
          new AccumulateFilter { FieldName = AccumulateField.AccumulateField_Eps, FilterMin = 1.0, FilterMax = 5.0, Period = AccumulatePeriod.Last_Year }
        },
        SortFieldData = new SortFieldData
        {
          FieldName = 1,
          Period = 0,
          FieldType = FieldBelongType.StockField_Type,
          SortDir = SortDir.SortDir_Ascend
        },
        Page = 1,
        PageSize = 20
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"base_filter_list\""));
      Assert.That(json, Does.Contain("\"accumulate_filter_list\""));
      Assert.That(json, Does.Contain("\"sort_field_data\""));
      Assert.That(json, Does.Contain("\"page\":1"));
      Assert.That(json, Does.Contain("\"page_size\":20"));
      // BaseFilter wire names
      Assert.That(json, Does.Contain("\"filter_min\":10"));
      Assert.That(json, Does.Contain("\"filter_max\":20"));
      // AccumulateFilter wire names
      Assert.That(json, Does.Contain("\"period\""));
    }

    [Test]
    public void BaseFilter_DefaultIsNoFilter_IsFalse()
    {
      var filter = new BaseFilter();
      Assert.That(filter.IsNoFilter, Is.False);
      Assert.That(filter.FilterMin, Is.EqualTo(0.0));
      Assert.That(filter.FilterMax, Is.EqualTo(0.0));
    }

    [Test]
    public void AccumulateFilter_DefaultIsNoFilter_IsFalse()
    {
      var filter = new AccumulateFilter();
      Assert.That(filter.IsNoFilter, Is.False);
    }

    [Test]
    public void FinancialFilter_DefaultIsNoFilter_IsFalse()
    {
      var filter = new FinancialFilter();
      Assert.That(filter.IsNoFilter, Is.False);
    }

    [Test]
    public void MultiTagsRelationFilter_DefaultIsNoFilter_IsFalse()
    {
      var filter = new MultiTagsRelationFilter();
      Assert.That(filter.IsNoFilter, Is.False);
      Assert.That(filter.TagList, Is.Null);
    }

    [Test]
    public void SortFieldData_DefaultConstructor_PropertiesNullOrDefault()
    {
      var data = new SortFieldData();
      Assert.That(data.FieldName, Is.EqualTo(0));
      Assert.That(data.Period, Is.EqualTo(0));
      // FieldBelongType has no zero member; default is (FieldBelongType)0
      Assert.That(data.FieldType, Is.EqualTo((FieldBelongType)0));
      Assert.That(data.SortDir, Is.EqualTo(SortDir.SortDir_No));
    }

    [Test]
    public void SortFieldData_Serialization_WireNamesPresent()
    {
      var data = new SortFieldData
      {
        FieldName = 11,
        Period = 6,
        FieldType = FieldBelongType.AccumulateField_Type,
        SortDir = SortDir.SortDir_Descend
      };
      string json = JsonConvert.SerializeObject(data, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"field_name\":11"));
      Assert.That(json, Does.Contain("\"period\":6"));
      Assert.That(json, Does.Contain("\"field_type\":\"AccumulateField_Type\""));
      Assert.That(json, Does.Contain("\"sort_dir\":\"SortDir_Descend\""));
    }

    // ---- MarketScannerTagsModel ----

    [Test]
    public void MarketScannerTagsModel_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new MarketScannerTagsModel();
      Assert.That(model.Market, Is.EqualTo(Market.NONE));
      Assert.That(model.MultiTagFieldList, Is.Null);
    }

    [Test]
    public void MarketScannerTagsModel_Serialization_WireNamesPresent()
    {
      // MarketScannerTagsModel.MultiTagFieldList is List<MultiTagField> so
      // each entry serializes to the C# field name (Java/Python
      // "field_request_name"), e.g. "MultiTagField_Industry".
      var model = new MarketScannerTagsModel
      {
        Market = Market.US,
        MultiTagFieldList = new List<MultiTagField>
        {
          MultiTagField.MultiTagField_Industry,
          MultiTagField.MultiTagField_Concept
        }
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"multi_tag_field_list\""));
      Assert.That(json, Does.Contain("MultiTagField_Industry"));
      Assert.That(json, Does.Contain("MultiTagField_Concept"));
    }

    // ---- OptionChainV3Model ----

    [Test]
    public void OptionChainV3Model_DefaultConstructor_PropertiesNullOrDefault()
    {
      var model = new OptionChainV3Model();
      Assert.That(model.Market, Is.EqualTo(Market.NONE));
      Assert.That(model.OptionBasic, Is.Null);
      Assert.That(model.OptionFilter, Is.Null);
      Assert.That(model.ReturnGreekValue, Is.False);
    }

    [Test]
    public void OptionChainV3Model_Serialization_WireNamesPresent()
    {
      var model = new OptionChainV3Model
      {
        Market = Market.US,
        OptionBasic = new List<OptionChainModel>
        {
          new OptionChainModel { Symbol = "AAPL", Expiry = 20240119L }
        },
        OptionFilter = new OptionChainFilterModel { InTheMoney = true },
        ReturnGreekValue = true
      };
      string json = JsonConvert.SerializeObject(model, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"option_basic\""));
      Assert.That(json, Does.Contain("\"option_filter\""));
      Assert.That(json, Does.Contain("\"return_greek_value\":true"));
      Assert.That(json, Does.Contain("\"in_the_money\":true"));
    }

    // ================================================================
    //  Enum classes
    // ================================================================

    // ---- PartCode / PartCodeExtensions ----
    // NOTE: PartCodeExtensions has a static constructor that performs
    //   Array.Sort(codes, (IComparer<string>?)System.Enum.GetValues(typeof(PartCode)).Cast<PartCode>())
    // which throws InvalidCastException at runtime (PartCode[] is not IComparer<string>).
    // Therefore ANY access to PartCodeExtensions members triggers a
    // System.TypeInitializationException. This is the actual source behavior
    // and is documented here as a regression guard — the tests assert the
    // throw, not the intended lookup logic.

    [Test]
    public void PartCodeExtensions_OfString_ThrowsTypeInitialization()
    {
      Assert.Throws<TypeInitializationException>(() => PartCodeExtensions.Of("a"));
    }

    [Test]
    public void PartCodeExtensions_OfString_UnknownCode_ThrowsTypeInitialization()
    {
      Assert.Throws<TypeInitializationException>(() => PartCodeExtensions.Of("not_a_code"));
    }

    [Test]
    public void PartCodeExtensions_OfString_EmptyCode_ThrowsTypeInitialization()
    {
      Assert.Throws<TypeInitializationException>(() => PartCodeExtensions.Of(""));
    }

    [Test]
    public void PartCodeExtensions_OfInt_AnyIndex_ThrowsTypeInitialization()
    {
      // The type initializer runs before the index range check, so even
      // valid indices trigger TypeInitializationException.
      Assert.Throws<TypeInitializationException>(() => PartCodeExtensions.Of(0));
    }

    [Test]
    public void PartCodeExtensions_OfInt_NegativeIndex_ThrowsTypeInitialization()
    {
      Assert.Throws<TypeInitializationException>(() => PartCodeExtensions.Of(-1));
    }

    [Test]
    public void PartCodeExtensions_OfInt_OutOfRangeIndex_ThrowsTypeInitialization()
    {
      Assert.Throws<TypeInitializationException>(() => PartCodeExtensions.Of(17));
      Assert.Throws<TypeInitializationException>(() => PartCodeExtensions.Of(100));
    }

    [Test]
    public void PartCodeExtensions_IndexExtension_ThrowsTypeInitialization()
    {
      Assert.Throws<TypeInitializationException>(() => PartCode.AMEX.Index());
      Assert.Throws<TypeInitializationException>(() => PartCode.MEMX.Index());
    }

    [Test]
    public void PartCode_EnumValues_AreSequential()
    {
      // The PartCode enum itself (separate from the broken Extensions class)
      // is a plain enum with sequential values from AMEX(0) to MEMX(16).
      Assert.That((int)PartCode.AMEX, Is.EqualTo(0));
      Assert.That((int)PartCode.BOX, Is.EqualTo(1));
      Assert.That((int)PartCode.CBOE, Is.EqualTo(2));
      Assert.That((int)PartCode.MEMX, Is.EqualTo(16));
    }

    // ---- FutureKType ----

    [Test]
    public void FutureKType_Values_ReturnsAllDeclaredInstances()
    {
      List<FutureKType> values = new List<FutureKType>(FutureKType.Values);
      Assert.That(values.Count, Is.EqualTo(16));
    }

    [Test]
    public void FutureKType_Values_IsIdempotent_SameReferences()
    {
      FutureKType firstA = new List<FutureKType>(FutureKType.Values)[0];
      FutureKType firstB = new List<FutureKType>(FutureKType.Values)[0];
      Assert.That(ReferenceEquals(firstA, firstB), Is.True);
    }

    [Test]
    public void FutureKType_Min1_Value_IsMin()
    {
      Assert.That(FutureKType.min1.Value, Is.EqualTo("min"));
    }

    [Test]
    public void FutureKType_Day_Value_IsDay()
    {
      Assert.That(FutureKType.day.Value, Is.EqualTo("day"));
    }

    [Test]
    public void FutureKType_Month_Value_IsMonth()
    {
      Assert.That(FutureKType.month.Value, Is.EqualTo("month"));
    }

    [Test]
    public void FutureKType_Values_FirstAndLast()
    {
      List<FutureKType> values = new List<FutureKType>(FutureKType.Values);
      Assert.That(values[0].Value, Is.EqualTo("min"));
      Assert.That(values[values.Count - 1].Value, Is.EqualTo("month"));
    }

    // ---- StockRankingIndicator ----

    [Test]
    public void StockRankingIndicator_Values_ReturnsAllDeclaredInstances()
    {
      List<StockRankingIndicator> values = new List<StockRankingIndicator>(StockRankingIndicator.Values);
      Assert.That(values.Count, Is.EqualTo(6));
    }

    [Test]
    public void StockRankingIndicator_ChangeRate_Value_IsChangeRate()
    {
      Assert.That(StockRankingIndicator.ChangeRate.Value, Is.EqualTo("changeRate"));
      Assert.That(StockRankingIndicator.ChangeRate.GetValue(), Is.EqualTo("changeRate"));
    }

    [Test]
    public void StockRankingIndicator_Volume_Value_IsVolume()
    {
      Assert.That(StockRankingIndicator.Volume.Value, Is.EqualTo("volume"));
    }

    [Test]
    public void StockRankingIndicator_Amplitude_Value_IsAmplitude()
    {
      Assert.That(StockRankingIndicator.Amplitude.Value, Is.EqualTo("amplitude"));
    }

    [Test]
    public void StockRankingIndicator_Values_IsIdempotent_SameReferences()
    {
      StockRankingIndicator firstA = new List<StockRankingIndicator>(StockRankingIndicator.Values)[0];
      StockRankingIndicator firstB = new List<StockRankingIndicator>(StockRankingIndicator.Values)[0];
      Assert.That(ReferenceEquals(firstA, firstB), Is.True);
    }

    [Test]
    public void StockRankingIndicator_GetIndicatorByValue_KnownValue_ReturnsInstance()
    {
      StockRankingIndicator? result = StockRankingIndicator.getIndicatorByValue("volume");
      Assert.That(result, Is.Not.Null);
      Assert.That(result, Is.EqualTo(StockRankingIndicator.Volume));
    }

    [Test]
    public void StockRankingIndicator_GetIndicatorByValue_UnknownValue_ReturnsNull()
    {
      StockRankingIndicator? result = StockRankingIndicator.getIndicatorByValue("nonexistent");
      Assert.That(result, Is.Null);
    }

    [Test]
    public void StockRankingIndicator_ImplementsIndicatorInterface()
    {
      Assert.That(StockRankingIndicator.ChangeRate, Is.InstanceOf<Indicator>());
      Assert.That(StockRankingIndicator.Volume, Is.InstanceOf<Indicator>());
    }

    [Test]
    public void StockRankingIndicator_Values_FirstAndLast()
    {
      List<StockRankingIndicator> values = new List<StockRankingIndicator>(StockRankingIndicator.Values);
      Assert.That(values[0].Value, Is.EqualTo("changeRate"));
      Assert.That(values[values.Count - 1].Value, Is.EqualTo("amplitude"));
    }

    // ---- OptionRankingIndicator ----

    [Test]
    public void OptionRankingIndicator_Values_ReturnsAllDeclaredInstances()
    {
      List<OptionRankingIndicator> values = new List<OptionRankingIndicator>(OptionRankingIndicator.Values);
      Assert.That(values.Count, Is.EqualTo(4));
    }

    [Test]
    public void OptionRankingIndicator_BigOrder_Value_IsBigOrder()
    {
      Assert.That(OptionRankingIndicator.BigOrder.Value, Is.EqualTo("bigOrder"));
      Assert.That(OptionRankingIndicator.BigOrder.GetValue(), Is.EqualTo("bigOrder"));
    }

    [Test]
    public void OptionRankingIndicator_OpenInt_Value_IsOpenInt()
    {
      Assert.That(OptionRankingIndicator.OpenInt.Value, Is.EqualTo("openInt"));
    }

    [Test]
    public void OptionRankingIndicator_Values_IsIdempotent_SameReferences()
    {
      OptionRankingIndicator firstA = new List<OptionRankingIndicator>(OptionRankingIndicator.Values)[0];
      OptionRankingIndicator firstB = new List<OptionRankingIndicator>(OptionRankingIndicator.Values)[0];
      Assert.That(ReferenceEquals(firstA, firstB), Is.True);
    }

    [Test]
    public void OptionRankingIndicator_GetIndicatorByValue_KnownValue_ReturnsInstance()
    {
      OptionRankingIndicator? result = OptionRankingIndicator.getIndicatorByValue("amount");
      Assert.That(result, Is.Not.Null);
      Assert.That(result, Is.EqualTo(OptionRankingIndicator.Amount));
    }

    [Test]
    public void OptionRankingIndicator_GetIndicatorByValue_UnknownValue_ReturnsNull()
    {
      OptionRankingIndicator? result = OptionRankingIndicator.getIndicatorByValue("nonexistent");
      Assert.That(result, Is.Null);
    }

    [Test]
    public void OptionRankingIndicator_ImplementsIndicatorInterface()
    {
      Assert.That(OptionRankingIndicator.BigOrder, Is.InstanceOf<Indicator>());
      Assert.That(OptionRankingIndicator.Volume, Is.InstanceOf<Indicator>());
    }

    [Test]
    public void OptionRankingIndicator_Values_FirstAndLast()
    {
      List<OptionRankingIndicator> values = new List<OptionRankingIndicator>(OptionRankingIndicator.Values);
      Assert.That(values[0].Value, Is.EqualTo("bigOrder"));
      Assert.That(values[values.Count - 1].Value, Is.EqualTo("openInt"));
    }

    // ---- Indicator interface (static GetValues) ----

    [Test]
    public void Indicator_GetValues_NullSet_ReturnsEmptySet()
    {
      ISet<string> values = Indicator.GetValues(null);
      Assert.That(values, Is.Not.Null);
      Assert.That(values.Count, Is.EqualTo(0));
    }

    [Test]
    public void Indicator_GetValues_EmptySet_ReturnsEmptySet()
    {
      ISet<Indicator> indicators = new HashSet<Indicator>();
      ISet<string> values = Indicator.GetValues(indicators);
      Assert.That(values, Is.Not.Null);
      Assert.That(values.Count, Is.EqualTo(0));
    }

    [Test]
    public void Indicator_GetValues_MixedIndicators_ReturnsWireStrings()
    {
      ISet<Indicator> indicators = new HashSet<Indicator>
      {
        StockRankingIndicator.Volume,
        OptionRankingIndicator.Amount,
        StockRankingIndicator.ChangeRate
      };
      ISet<string> values = Indicator.GetValues(indicators);
      Assert.That(values.Count, Is.EqualTo(3));
      Assert.That(values, Does.Contain("volume"));
      Assert.That(values, Does.Contain("amount"));
      Assert.That(values, Does.Contain("changeRate"));
    }

    // ---- HourTradingTimelineModel ----

    [Test]
    public void HourTradingTimelineModel_Serialization_UsesSingularSymbol()
    {
      // hour_trading_timeline wire uses the singular `symbol` key (not the
      // `symbols` array used by QuoteSymbolModel), plus optional begin_time.
      var m = new HourTradingTimelineModel { Symbol = "AAPL", BeginTime = 1700000000000 };
      string json = JsonConvert.SerializeObject(m, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Contain("\"begin_time\":1700000000000"));
      Assert.That(json, Does.Not.Contain("\"symbols\""));
    }

    [Test]
    public void HourTradingTimelineModel_Serialization_OmitsUnsetBeginTime()
    {
      // begin_time is optional — NullValueHandling.Ignore keeps it out of
      // the payload when unset.
      var m = new HourTradingTimelineModel { Symbol = "AAPL" };
      string json = JsonConvert.SerializeObject(m, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbol\":\"AAPL\""));
      Assert.That(json, Does.Not.Contain("begin_time"));
    }

    // ---- FinancialDailyModel ----

    [Test]
    public void FinancialDailyModel_Serialization_WireNamesPresent()
    {
      var m = new FinancialDailyModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US,
        Fields = new List<string> { "open_price" },
        BeginDate = 1704067200000,
        EndDate = 1735689599000
      };
      string json = JsonConvert.SerializeObject(m, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbols\":[\"AAPL\"]"));
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"fields\":[\"open_price\"]"));
      Assert.That(json, Does.Contain("\"begin_date\":1704067200000"));
      Assert.That(json, Does.Contain("\"end_date\":1735689599000"));
    }

    // ---- FinancialReportModel ----

    [Test]
    public void FinancialReportModel_Serialization_PeriodTypeAsEnumName()
    {
      // period_type uses StringEnumConverter — serializes to enum member
      // name ("Quarterly"/"Annual"/"LTM"), matching Java wire.
      var m = new FinancialReportModel
      {
        Symbols = new List<string> { "AAPL" },
        Market = Market.US,
        Fields = new List<string> { "total_revenue" },
        PeriodType = FinancialPeriodType.Quarterly
      };
      string json = JsonConvert.SerializeObject(m, TigerClient.JsonSet);
      Assert.That(json, Does.Contain("\"symbols\":[\"AAPL\"]"));
      Assert.That(json, Does.Contain("\"market\":\"US\""));
      Assert.That(json, Does.Contain("\"fields\":[\"total_revenue\"]"));
      Assert.That(json, Does.Contain("\"period_type\":\"Quarterly\""));
      // begin_date/end_date default to null — omitted via NullValueHandling.
      Assert.That(json, Does.Not.Contain("begin_date"));
      Assert.That(json, Does.Not.Contain("end_date"));
    }

    [Test]
    public void FinancialReportModel_Serialization_AllPeriodTypesRoundtrip()
    {
      foreach (var pt in new[] { FinancialPeriodType.Annual, FinancialPeriodType.Quarterly, FinancialPeriodType.LTM })
      {
        var m = new FinancialReportModel
        {
          Symbols = new List<string> { "AAPL" },
          Market = Market.US,
          Fields = new List<string> { "revenues" },
          PeriodType = pt
        };
        string json = JsonConvert.SerializeObject(m, TigerClient.JsonSet);
        Assert.That(json, Does.Contain($"\"period_type\":\"{pt}\""),
            $"period_type must round-trip for {pt}");
      }
    }
  }
}
