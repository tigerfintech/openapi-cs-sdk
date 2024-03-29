// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: QuoteDepthData.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace TigerOpenAPI.Quote.Pb {

  /// <summary>Holder for reflection information generated from QuoteDepthData.proto</summary>
  public static partial class QuoteDepthDataReflection {

    #region Descriptor
    /// <summary>File descriptor for QuoteDepthData.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static QuoteDepthDataReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRRdW90ZURlcHRoRGF0YS5wcm90bxI0Y29tLnRpZ2VyYnJva2Vycy5zdG9j",
            "ay5vcGVuYXBpLmNsaWVudC5zb2NrZXQuZGF0YS5wYiLNAgoOUXVvdGVEZXB0",
            "aERhdGESDgoGc3ltYm9sGAEgASgJEhEKCXRpbWVzdGFtcBgCIAEoBBJbCgNh",
            "c2sYAyABKAsyTi5jb20udGlnZXJicm9rZXJzLnN0b2NrLm9wZW5hcGkuY2xp",
            "ZW50LnNvY2tldC5kYXRhLnBiLlF1b3RlRGVwdGhEYXRhLk9yZGVyQm9vaxJb",
            "CgNiaWQYBCABKAsyTi5jb20udGlnZXJicm9rZXJzLnN0b2NrLm9wZW5hcGku",
            "Y2xpZW50LnNvY2tldC5kYXRhLnBiLlF1b3RlRGVwdGhEYXRhLk9yZGVyQm9v",
            "axpeCglPcmRlckJvb2sSDQoFcHJpY2UYASADKAESDgoGdm9sdW1lGAIgAygS",
            "EhIKCm9yZGVyQ291bnQYAyADKA0SEAoIZXhjaGFuZ2UYBCADKAkSDAoEdGlt",
            "ZRgFIAMoEkIYqgIVVGlnZXJPcGVuQVBJLlF1b3RlLlBiYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::TigerOpenAPI.Quote.Pb.QuoteDepthData), global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Parser, new[]{ "Symbol", "Timestamp", "Ask", "Bid" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook), global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook.Parser, new[]{ "Price", "Volume", "OrderCount", "Exchange", "Time" }, null, null, null, null)})
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class QuoteDepthData : pb::IMessage<QuoteDepthData>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<QuoteDepthData> _parser = new pb::MessageParser<QuoteDepthData>(() => new QuoteDepthData());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<QuoteDepthData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::TigerOpenAPI.Quote.Pb.QuoteDepthDataReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public QuoteDepthData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public QuoteDepthData(QuoteDepthData other) : this() {
      symbol_ = other.symbol_;
      timestamp_ = other.timestamp_;
      ask_ = other.ask_ != null ? other.ask_.Clone() : null;
      bid_ = other.bid_ != null ? other.bid_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public QuoteDepthData Clone() {
      return new QuoteDepthData(this);
    }

    /// <summary>Field number for the "symbol" field.</summary>
    public const int SymbolFieldNumber = 1;
    private string symbol_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Symbol {
      get { return symbol_; }
      set {
        symbol_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "timestamp" field.</summary>
    public const int TimestampFieldNumber = 2;
    private ulong timestamp_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ulong Timestamp {
      get { return timestamp_; }
      set {
        timestamp_ = value;
      }
    }

    /// <summary>Field number for the "ask" field.</summary>
    public const int AskFieldNumber = 3;
    private global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook ask_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook Ask {
      get { return ask_; }
      set {
        ask_ = value;
      }
    }

    /// <summary>Field number for the "bid" field.</summary>
    public const int BidFieldNumber = 4;
    private global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook bid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook Bid {
      get { return bid_; }
      set {
        bid_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as QuoteDepthData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(QuoteDepthData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Symbol != other.Symbol) return false;
      if (Timestamp != other.Timestamp) return false;
      if (!object.Equals(Ask, other.Ask)) return false;
      if (!object.Equals(Bid, other.Bid)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Symbol.Length != 0) hash ^= Symbol.GetHashCode();
      if (Timestamp != 0UL) hash ^= Timestamp.GetHashCode();
      if (ask_ != null) hash ^= Ask.GetHashCode();
      if (bid_ != null) hash ^= Bid.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Symbol.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Symbol);
      }
      if (Timestamp != 0UL) {
        output.WriteRawTag(16);
        output.WriteUInt64(Timestamp);
      }
      if (ask_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Ask);
      }
      if (bid_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Bid);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Symbol.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Symbol);
      }
      if (Timestamp != 0UL) {
        output.WriteRawTag(16);
        output.WriteUInt64(Timestamp);
      }
      if (ask_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Ask);
      }
      if (bid_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(Bid);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Symbol.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Symbol);
      }
      if (Timestamp != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(Timestamp);
      }
      if (ask_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Ask);
      }
      if (bid_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Bid);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(QuoteDepthData other) {
      if (other == null) {
        return;
      }
      if (other.Symbol.Length != 0) {
        Symbol = other.Symbol;
      }
      if (other.Timestamp != 0UL) {
        Timestamp = other.Timestamp;
      }
      if (other.ask_ != null) {
        if (ask_ == null) {
          Ask = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook();
        }
        Ask.MergeFrom(other.Ask);
      }
      if (other.bid_ != null) {
        if (bid_ == null) {
          Bid = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook();
        }
        Bid.MergeFrom(other.Bid);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Symbol = input.ReadString();
            break;
          }
          case 16: {
            Timestamp = input.ReadUInt64();
            break;
          }
          case 26: {
            if (ask_ == null) {
              Ask = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook();
            }
            input.ReadMessage(Ask);
            break;
          }
          case 34: {
            if (bid_ == null) {
              Bid = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook();
            }
            input.ReadMessage(Bid);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            Symbol = input.ReadString();
            break;
          }
          case 16: {
            Timestamp = input.ReadUInt64();
            break;
          }
          case 26: {
            if (ask_ == null) {
              Ask = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook();
            }
            input.ReadMessage(Ask);
            break;
          }
          case 34: {
            if (bid_ == null) {
              Bid = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Types.OrderBook();
            }
            input.ReadMessage(Bid);
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    /// <summary>Container for nested types declared in the QuoteDepthData message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public sealed partial class OrderBook : pb::IMessage<OrderBook>
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          , pb::IBufferMessage
      #endif
      {
        private static readonly pb::MessageParser<OrderBook> _parser = new pb::MessageParser<OrderBook>(() => new OrderBook());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pb::MessageParser<OrderBook> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::TigerOpenAPI.Quote.Pb.QuoteDepthData.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public OrderBook() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public OrderBook(OrderBook other) : this() {
          price_ = other.price_.Clone();
          volume_ = other.volume_.Clone();
          orderCount_ = other.orderCount_.Clone();
          exchange_ = other.exchange_.Clone();
          time_ = other.time_.Clone();
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public OrderBook Clone() {
          return new OrderBook(this);
        }

        /// <summary>Field number for the "price" field.</summary>
        public const int PriceFieldNumber = 1;
        private static readonly pb::FieldCodec<double> _repeated_price_codec
            = pb::FieldCodec.ForDouble(10);
        private readonly pbc::RepeatedField<double> price_ = new pbc::RepeatedField<double>();
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public pbc::RepeatedField<double> Price {
          get { return price_; }
        }

        /// <summary>Field number for the "volume" field.</summary>
        public const int VolumeFieldNumber = 2;
        private static readonly pb::FieldCodec<long> _repeated_volume_codec
            = pb::FieldCodec.ForSInt64(18);
        private readonly pbc::RepeatedField<long> volume_ = new pbc::RepeatedField<long>();
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public pbc::RepeatedField<long> Volume {
          get { return volume_; }
        }

        /// <summary>Field number for the "orderCount" field.</summary>
        public const int OrderCountFieldNumber = 3;
        private static readonly pb::FieldCodec<uint> _repeated_orderCount_codec
            = pb::FieldCodec.ForUInt32(26);
        private readonly pbc::RepeatedField<uint> orderCount_ = new pbc::RepeatedField<uint>();
        /// <summary>
        /// required when symbol in HK market
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public pbc::RepeatedField<uint> OrderCount {
          get { return orderCount_; }
        }

        /// <summary>Field number for the "exchange" field.</summary>
        public const int ExchangeFieldNumber = 4;
        private static readonly pb::FieldCodec<string> _repeated_exchange_codec
            = pb::FieldCodec.ForString(34);
        private readonly pbc::RepeatedField<string> exchange_ = new pbc::RepeatedField<string>();
        /// <summary>
        /// option exchange
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public pbc::RepeatedField<string> Exchange {
          get { return exchange_; }
        }

        /// <summary>Field number for the "time" field.</summary>
        public const int TimeFieldNumber = 5;
        private static readonly pb::FieldCodec<long> _repeated_time_codec
            = pb::FieldCodec.ForSInt64(42);
        private readonly pbc::RepeatedField<long> time_ = new pbc::RepeatedField<long>();
        /// <summary>
        /// option exchange time
        /// </summary>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public pbc::RepeatedField<long> Time {
          get { return time_; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override bool Equals(object other) {
          return Equals(other as OrderBook);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool Equals(OrderBook other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if(!price_.Equals(other.price_)) return false;
          if(!volume_.Equals(other.volume_)) return false;
          if(!orderCount_.Equals(other.orderCount_)) return false;
          if(!exchange_.Equals(other.exchange_)) return false;
          if(!time_.Equals(other.time_)) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override int GetHashCode() {
          int hash = 1;
          hash ^= price_.GetHashCode();
          hash ^= volume_.GetHashCode();
          hash ^= orderCount_.GetHashCode();
          hash ^= exchange_.GetHashCode();
          hash ^= time_.GetHashCode();
          if (_unknownFields != null) {
            hash ^= _unknownFields.GetHashCode();
          }
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void WriteTo(pb::CodedOutputStream output) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          output.WriteRawMessage(this);
        #else
          price_.WriteTo(output, _repeated_price_codec);
          volume_.WriteTo(output, _repeated_volume_codec);
          orderCount_.WriteTo(output, _repeated_orderCount_codec);
          exchange_.WriteTo(output, _repeated_exchange_codec);
          time_.WriteTo(output, _repeated_time_codec);
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
          price_.WriteTo(ref output, _repeated_price_codec);
          volume_.WriteTo(ref output, _repeated_volume_codec);
          orderCount_.WriteTo(ref output, _repeated_orderCount_codec);
          exchange_.WriteTo(ref output, _repeated_exchange_codec);
          time_.WriteTo(ref output, _repeated_time_codec);
          if (_unknownFields != null) {
            _unknownFields.WriteTo(ref output);
          }
        }
        #endif

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int CalculateSize() {
          int size = 0;
          size += price_.CalculateSize(_repeated_price_codec);
          size += volume_.CalculateSize(_repeated_volume_codec);
          size += orderCount_.CalculateSize(_repeated_orderCount_codec);
          size += exchange_.CalculateSize(_repeated_exchange_codec);
          size += time_.CalculateSize(_repeated_time_codec);
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(OrderBook other) {
          if (other == null) {
            return;
          }
          price_.Add(other.price_);
          volume_.Add(other.volume_);
          orderCount_.Add(other.orderCount_);
          exchange_.Add(other.exchange_);
          time_.Add(other.time_);
          _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(pb::CodedInputStream input) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          input.ReadRawMessage(this);
        #else
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                break;
              case 10:
              case 9: {
                price_.AddEntriesFrom(input, _repeated_price_codec);
                break;
              }
              case 18:
              case 16: {
                volume_.AddEntriesFrom(input, _repeated_volume_codec);
                break;
              }
              case 26:
              case 24: {
                orderCount_.AddEntriesFrom(input, _repeated_orderCount_codec);
                break;
              }
              case 34: {
                exchange_.AddEntriesFrom(input, _repeated_exchange_codec);
                break;
              }
              case 42:
              case 40: {
                time_.AddEntriesFrom(input, _repeated_time_codec);
                break;
              }
            }
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                break;
              case 10:
              case 9: {
                price_.AddEntriesFrom(ref input, _repeated_price_codec);
                break;
              }
              case 18:
              case 16: {
                volume_.AddEntriesFrom(ref input, _repeated_volume_codec);
                break;
              }
              case 26:
              case 24: {
                orderCount_.AddEntriesFrom(ref input, _repeated_orderCount_codec);
                break;
              }
              case 34: {
                exchange_.AddEntriesFrom(ref input, _repeated_exchange_codec);
                break;
              }
              case 42:
              case 40: {
                time_.AddEntriesFrom(ref input, _repeated_time_codec);
                break;
              }
            }
          }
        }
        #endif

      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
