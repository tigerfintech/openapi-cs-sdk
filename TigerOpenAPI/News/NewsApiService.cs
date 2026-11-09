using System;
using System.Collections.Generic;

namespace TigerOpenAPI.News
{
  /// <summary>
  /// News domain api method registry (news_stock_list / news_highlight_list).
  /// Mirrors QuoteApiService / TradeApiService: the method-name constants double
  /// as the registry used by NewsClient.Validate to reject foreign methods.
  /// Contract: stock-openapi docs/news_api_design.md §3.
  /// </summary>
  public class NewsApiService
  {

    /**
     * news
     */
    public const string NEWS_STOCK_LIST = "news_stock_list";
    public const string NEWS_HIGHLIGHT_LIST = "news_highlight_list";

    public static readonly HashSet<string> AllNewsApiSet = new HashSet<string>() {
      NEWS_STOCK_LIST,
      NEWS_HIGHLIGHT_LIST
    };

    public static bool IsNewsApi(in string newsApi) =>
      string.IsNullOrWhiteSpace(newsApi) ? false : AllNewsApiSet.Contains(newsApi);

    NewsApiService()
    {
    }
  }
}
