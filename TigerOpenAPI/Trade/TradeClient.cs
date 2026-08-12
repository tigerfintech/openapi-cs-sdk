using System;
using TigerOpenAPI.Common;
using TigerOpenAPI.Common.Enum;
using TigerOpenAPI.Common.Util;
using TigerOpenAPI.Config;
using TigerOpenAPI.Model;
using TigerOpenAPI.Quote;
using TigerOpenAPI.Trade.Model;
using TigerOpenAPI.Trade.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TigerOpenAPI.Trade
{
  public class TradeClient : TigerClient
  {
    private string ServerUrl { get; set; }
    private string ServerUrlForPaper { get; set; }

    public TradeClient(TigerConfig config) : base(config)
    {
      ApiLogger.Debug($"TradeClient env:{config.Environment}, license:{config.License}");
      RefreshServerUri(config);

      TokenManager.GetInstance().Init(config, this);
    }

    protected override void RefreshServerUri(TigerConfig config)
    {
      // get serverAddress by Env and license
      Dictionary<UriType, string> uriDict = NetworkUtil.GetServerAddress(Protocol.HTTP, config.License, config.Environment);
      ServerUrl = (uriDict.ContainsKey(UriType.TRADE) && !string.IsNullOrWhiteSpace(uriDict[UriType.TRADE]))
        ? uriDict[UriType.TRADE] : uriDict[UriType.COMMON];
      ServerUrlForPaper = (uriDict.ContainsKey(UriType.PAPER) && !string.IsNullOrWhiteSpace(uriDict[UriType.PAPER]))
        ? uriDict[UriType.PAPER] : uriDict[UriType.COMMON];
    }

    public override string GetServerUri<T>(TigerRequest<T> request)
    {
      return AccountUtil.IsVirtualAccount(request?.ModelValue?.Account) ? ServerUrlForPaper : ServerUrl;
    }

    public override bool Validate<T>(TigerRequest<T> request, out string errorMsg)
    {
      errorMsg = string.Empty;
      if (QuoteApiService.USER_TOKEN_REFRESH.Equals(request.ApiMethodName))
      {
        return true;
      }
      if (!TradeApiService.IsTradeApi(request.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_COMMON_PARAM_ERROR.Message, $"'ApiMethodName'({request?.ApiMethodName}) is not trade api");
        return false;
      }
      if (request.ModelValue != null && !string.Equals(TradeApiService.ACCOUNTS, request.ApiMethodName)
        && string.IsNullOrWhiteSpace(request.ModelValue.Account))
      {
        request.ModelValue.Account = Config.DefaultAccount;
      }
      if (request.ModelValue != null && request.ModelValue is TradeModel)
      {
        TradeModel tradeModel = (TradeModel)request.ModelValue;
        if (string.IsNullOrWhiteSpace(tradeModel.SecretKey)
          && !string.IsNullOrWhiteSpace(Config.SecretKey))
        {
          tradeModel.SecretKey = Config.SecretKey;
        }
      }

      if (string.IsNullOrWhiteSpace(request.ModelValue?.Account)
        && !string.Equals(TradeApiService.ACCOUNTS, request.ApiMethodName))
      {
        errorMsg = string.Format(TigerApiCode.HTTP_BIZ_PARAM_EMPTY_ERROR.Message, "'Account'");
        return false;
      }
      // other param check
      return true;
    }

    /// <summary>Submit an option early exercise or abandon (expire) request.</summary>
    public Task<OptionExerciseSubmitResponse> SubmitOptionExerciseAsync(
      long contractId, string type, double quantity,
      string executingDate = null, bool? isForce = null, int? itmRate = null, string account = null)
    {
      var model = new OptionExerciseSubmitModel
      {
        Account = account,
        ContractId = contractId,
        Type = type,
        Quantity = quantity,
        ExecutingDate = executingDate,
        IsForce = isForce,
        ItmRate = itmRate,
      };
      var request = new TigerRequest<OptionExerciseSubmitResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_SUBMIT,
        ModelValue = model,
      };
      return ExecuteAsync(request);
    }

    /// <summary>Preview stock position changes from an exercise/expire request.</summary>
    public Task<OptionExerciseCheckResponse> CheckOptionExerciseAsync(
      long contractId, string type, double quantity,
      string executingDate = null, bool? isForce = null, int? itmRate = null, string account = null)
    {
      var model = new OptionExerciseCheckModel
      {
        Account = account,
        ContractId = contractId,
        Type = type,
        Quantity = quantity,
        ExecutingDate = executingDate,
        IsForce = isForce,
        ItmRate = itmRate,
      };
      var request = new TigerRequest<OptionExerciseCheckResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_CHECK,
        ModelValue = model,
      };
      return ExecuteAsync(request);
    }

    /// <summary>Query paginated option exercise/expire records.</summary>
    public Task<OptionExerciseRecordResponse> GetOptionExerciseRecordsAsync(
      string type = null, string status = null, string symbol = null,
      string orderBy = null, int page = 1, int size = 20, string account = null)
    {
      var model = new OptionExerciseRecordModel
      {
        Account = account,
        Type = type,
        Status = status,
        Symbol = symbol,
        OrderBy = orderBy,
        Page = page,
        Size = size,
      };
      var request = new TigerRequest<OptionExerciseRecordResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_RECORD,
        ModelValue = model,
      };
      return ExecuteAsync(request);
    }

    /// <summary>Query option positions available for exercise or abandon.</summary>
    public Task<OptionExercisePositionResponse> GetOptionExercisePositionsAsync(
      string type, string account = null)
    {
      var model = new OptionExercisePositionModel
      {
        Account = account,
        Type = type,
      };
      var request = new TigerRequest<OptionExercisePositionResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_POSITION,
        ModelValue = model,
      };
      return ExecuteAsync(request);
    }

    /// <summary>Cancel a pending option exercise request.</summary>
    public Task<OptionExerciseCancelResponse> CancelOptionExerciseAsync(
      long exerciseId, string account = null)
    {
      var model = new OptionExerciseCancelModel
      {
        Account = account,
        Id = exerciseId,
      };
      var request = new TigerRequest<OptionExerciseCancelResponse>
      {
        ApiMethodName = TradeApiService.OPTION_EXERCISE_CANCEL,
        ModelValue = model,
      };
      return ExecuteAsync(request);
    }
  }
}

