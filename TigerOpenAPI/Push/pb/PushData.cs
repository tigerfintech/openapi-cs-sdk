// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: PushData.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace TigerOpenAPI.Quote.Pb {

  /// <summary>Holder for reflection information generated from PushData.proto</summary>
  public static partial class PushDataReflection {

    #region Descriptor
    /// <summary>File descriptor for PushData.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PushDataReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg5QdXNoRGF0YS5wcm90bxI0Y29tLnRpZ2VyYnJva2Vycy5zdG9jay5vcGVu",
            "YXBpLmNsaWVudC5zb2NrZXQuZGF0YS5wYhoSU29ja2V0Q29tbW9uLnByb3Rv",
            "GhVPcmRlclN0YXR1c0RhdGEucHJvdG8aElBvc2l0aW9uRGF0YS5wcm90bxoP",
            "QXNzZXREYXRhLnByb3RvGg9RdW90ZURhdGEucHJvdG8aFFF1b3RlRGVwdGhE",
            "YXRhLnByb3RvGhNUcmFkZVRpY2tEYXRhLnByb3RvGhpPcmRlclRyYW5zYWN0",
            "aW9uRGF0YS5wcm90byKFBgoIUHVzaERhdGESXQoIZGF0YVR5cGUYASABKA4y",
            "Sy5jb20udGlnZXJicm9rZXJzLnN0b2NrLm9wZW5hcGkuY2xpZW50LnNvY2tl",
            "dC5kYXRhLnBiLlNvY2tldENvbW1vbi5EYXRhVHlwZRJUCglxdW90ZURhdGEY",
            "AiABKAsyPy5jb20udGlnZXJicm9rZXJzLnN0b2NrLm9wZW5hcGkuY2xpZW50",
            "LnNvY2tldC5kYXRhLnBiLlF1b3RlRGF0YUgAEl4KDnF1b3RlRGVwdGhEYXRh",
            "GAMgASgLMkQuY29tLnRpZ2VyYnJva2Vycy5zdG9jay5vcGVuYXBpLmNsaWVu",
            "dC5zb2NrZXQuZGF0YS5wYi5RdW90ZURlcHRoRGF0YUgAElwKDXRyYWRlVGlj",
            "a0RhdGEYBCABKAsyQy5jb20udGlnZXJicm9rZXJzLnN0b2NrLm9wZW5hcGku",
            "Y2xpZW50LnNvY2tldC5kYXRhLnBiLlRyYWRlVGlja0RhdGFIABJaCgxwb3Np",
            "dGlvbkRhdGEYBSABKAsyQi5jb20udGlnZXJicm9rZXJzLnN0b2NrLm9wZW5h",
            "cGkuY2xpZW50LnNvY2tldC5kYXRhLnBiLlBvc2l0aW9uRGF0YUgAElQKCWFz",
            "c2V0RGF0YRgGIAEoCzI/LmNvbS50aWdlcmJyb2tlcnMuc3RvY2sub3BlbmFw",
            "aS5jbGllbnQuc29ja2V0LmRhdGEucGIuQXNzZXREYXRhSAASYAoPb3JkZXJT",
            "dGF0dXNEYXRhGAcgASgLMkUuY29tLnRpZ2VyYnJva2Vycy5zdG9jay5vcGVu",
            "YXBpLmNsaWVudC5zb2NrZXQuZGF0YS5wYi5PcmRlclN0YXR1c0RhdGFIABJq",
            "ChRvcmRlclRyYW5zYWN0aW9uRGF0YRgIIAEoCzJKLmNvbS50aWdlcmJyb2tl",
            "cnMuc3RvY2sub3BlbmFwaS5jbGllbnQuc29ja2V0LmRhdGEucGIuT3JkZXJU",
            "cmFuc2FjdGlvbkRhdGFIAEIGCgRib2R5QhiqAhVUaWdlck9wZW5BUEkuUXVv",
            "dGUuUGJQAFABUAJQA1AEUAVQBlAHYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::TigerOpenAPI.Quote.Pb.SocketCommonReflection.Descriptor, global::TigerOpenAPI.Quote.Pb.OrderStatusDataReflection.Descriptor, global::TigerOpenAPI.Quote.Pb.PositionDataReflection.Descriptor, global::TigerOpenAPI.Quote.Pb.AssetDataReflection.Descriptor, global::TigerOpenAPI.Quote.Pb.QuoteDataReflection.Descriptor, global::TigerOpenAPI.Quote.Pb.QuoteDepthDataReflection.Descriptor, global::TigerOpenAPI.Quote.Pb.TradeTickDataReflection.Descriptor, global::TigerOpenAPI.Quote.Pb.OrderTransactionDataReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::TigerOpenAPI.Quote.Pb.PushData), global::TigerOpenAPI.Quote.Pb.PushData.Parser, new[]{ "DataType", "QuoteData", "QuoteDepthData", "TradeTickData", "PositionData", "AssetData", "OrderStatusData", "OrderTransactionData" }, new[]{ "Body" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class PushData : pb::IMessage<PushData>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PushData> _parser = new pb::MessageParser<PushData>(() => new PushData());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<PushData> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::TigerOpenAPI.Quote.Pb.PushDataReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PushData() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PushData(PushData other) : this() {
      dataType_ = other.dataType_;
      switch (other.BodyCase) {
        case BodyOneofCase.QuoteData:
          QuoteData = other.QuoteData.Clone();
          break;
        case BodyOneofCase.QuoteDepthData:
          QuoteDepthData = other.QuoteDepthData.Clone();
          break;
        case BodyOneofCase.TradeTickData:
          TradeTickData = other.TradeTickData.Clone();
          break;
        case BodyOneofCase.PositionData:
          PositionData = other.PositionData.Clone();
          break;
        case BodyOneofCase.AssetData:
          AssetData = other.AssetData.Clone();
          break;
        case BodyOneofCase.OrderStatusData:
          OrderStatusData = other.OrderStatusData.Clone();
          break;
        case BodyOneofCase.OrderTransactionData:
          OrderTransactionData = other.OrderTransactionData.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PushData Clone() {
      return new PushData(this);
    }

    /// <summary>Field number for the "dataType" field.</summary>
    public const int DataTypeFieldNumber = 1;
    private global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType dataType_ = global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType.Unknown;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType DataType {
      get { return dataType_; }
      set {
        dataType_ = value;
      }
    }

    /// <summary>Field number for the "quoteData" field.</summary>
    public const int QuoteDataFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.QuoteData QuoteData {
      get { return bodyCase_ == BodyOneofCase.QuoteData ? (global::TigerOpenAPI.Quote.Pb.QuoteData) body_ : null; }
      set {
        body_ = value;
        bodyCase_ = value == null ? BodyOneofCase.None : BodyOneofCase.QuoteData;
      }
    }

    /// <summary>Field number for the "quoteDepthData" field.</summary>
    public const int QuoteDepthDataFieldNumber = 3;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.QuoteDepthData QuoteDepthData {
      get { return bodyCase_ == BodyOneofCase.QuoteDepthData ? (global::TigerOpenAPI.Quote.Pb.QuoteDepthData) body_ : null; }
      set {
        body_ = value;
        bodyCase_ = value == null ? BodyOneofCase.None : BodyOneofCase.QuoteDepthData;
      }
    }

    /// <summary>Field number for the "tradeTickData" field.</summary>
    public const int TradeTickDataFieldNumber = 4;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.TradeTickData TradeTickData {
      get { return bodyCase_ == BodyOneofCase.TradeTickData ? (global::TigerOpenAPI.Quote.Pb.TradeTickData) body_ : null; }
      set {
        body_ = value;
        bodyCase_ = value == null ? BodyOneofCase.None : BodyOneofCase.TradeTickData;
      }
    }

    /// <summary>Field number for the "positionData" field.</summary>
    public const int PositionDataFieldNumber = 5;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.PositionData PositionData {
      get { return bodyCase_ == BodyOneofCase.PositionData ? (global::TigerOpenAPI.Quote.Pb.PositionData) body_ : null; }
      set {
        body_ = value;
        bodyCase_ = value == null ? BodyOneofCase.None : BodyOneofCase.PositionData;
      }
    }

    /// <summary>Field number for the "assetData" field.</summary>
    public const int AssetDataFieldNumber = 6;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.AssetData AssetData {
      get { return bodyCase_ == BodyOneofCase.AssetData ? (global::TigerOpenAPI.Quote.Pb.AssetData) body_ : null; }
      set {
        body_ = value;
        bodyCase_ = value == null ? BodyOneofCase.None : BodyOneofCase.AssetData;
      }
    }

    /// <summary>Field number for the "orderStatusData" field.</summary>
    public const int OrderStatusDataFieldNumber = 7;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.OrderStatusData OrderStatusData {
      get { return bodyCase_ == BodyOneofCase.OrderStatusData ? (global::TigerOpenAPI.Quote.Pb.OrderStatusData) body_ : null; }
      set {
        body_ = value;
        bodyCase_ = value == null ? BodyOneofCase.None : BodyOneofCase.OrderStatusData;
      }
    }

    /// <summary>Field number for the "orderTransactionData" field.</summary>
    public const int OrderTransactionDataFieldNumber = 8;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.OrderTransactionData OrderTransactionData {
      get { return bodyCase_ == BodyOneofCase.OrderTransactionData ? (global::TigerOpenAPI.Quote.Pb.OrderTransactionData) body_ : null; }
      set {
        body_ = value;
        bodyCase_ = value == null ? BodyOneofCase.None : BodyOneofCase.OrderTransactionData;
      }
    }

    private object body_;
    /// <summary>Enum of possible cases for the "body" oneof.</summary>
    public enum BodyOneofCase {
      None = 0,
      QuoteData = 2,
      QuoteDepthData = 3,
      TradeTickData = 4,
      PositionData = 5,
      AssetData = 6,
      OrderStatusData = 7,
      OrderTransactionData = 8,
    }
    private BodyOneofCase bodyCase_ = BodyOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public BodyOneofCase BodyCase {
      get { return bodyCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearBody() {
      bodyCase_ = BodyOneofCase.None;
      body_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as PushData);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(PushData other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (DataType != other.DataType) return false;
      if (!object.Equals(QuoteData, other.QuoteData)) return false;
      if (!object.Equals(QuoteDepthData, other.QuoteDepthData)) return false;
      if (!object.Equals(TradeTickData, other.TradeTickData)) return false;
      if (!object.Equals(PositionData, other.PositionData)) return false;
      if (!object.Equals(AssetData, other.AssetData)) return false;
      if (!object.Equals(OrderStatusData, other.OrderStatusData)) return false;
      if (!object.Equals(OrderTransactionData, other.OrderTransactionData)) return false;
      if (BodyCase != other.BodyCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (DataType != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType.Unknown) hash ^= DataType.GetHashCode();
      if (bodyCase_ == BodyOneofCase.QuoteData) hash ^= QuoteData.GetHashCode();
      if (bodyCase_ == BodyOneofCase.QuoteDepthData) hash ^= QuoteDepthData.GetHashCode();
      if (bodyCase_ == BodyOneofCase.TradeTickData) hash ^= TradeTickData.GetHashCode();
      if (bodyCase_ == BodyOneofCase.PositionData) hash ^= PositionData.GetHashCode();
      if (bodyCase_ == BodyOneofCase.AssetData) hash ^= AssetData.GetHashCode();
      if (bodyCase_ == BodyOneofCase.OrderStatusData) hash ^= OrderStatusData.GetHashCode();
      if (bodyCase_ == BodyOneofCase.OrderTransactionData) hash ^= OrderTransactionData.GetHashCode();
      hash ^= (int) bodyCase_;
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
      if (DataType != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType.Unknown) {
        output.WriteRawTag(8);
        output.WriteEnum((int) DataType);
      }
      if (bodyCase_ == BodyOneofCase.QuoteData) {
        output.WriteRawTag(18);
        output.WriteMessage(QuoteData);
      }
      if (bodyCase_ == BodyOneofCase.QuoteDepthData) {
        output.WriteRawTag(26);
        output.WriteMessage(QuoteDepthData);
      }
      if (bodyCase_ == BodyOneofCase.TradeTickData) {
        output.WriteRawTag(34);
        output.WriteMessage(TradeTickData);
      }
      if (bodyCase_ == BodyOneofCase.PositionData) {
        output.WriteRawTag(42);
        output.WriteMessage(PositionData);
      }
      if (bodyCase_ == BodyOneofCase.AssetData) {
        output.WriteRawTag(50);
        output.WriteMessage(AssetData);
      }
      if (bodyCase_ == BodyOneofCase.OrderStatusData) {
        output.WriteRawTag(58);
        output.WriteMessage(OrderStatusData);
      }
      if (bodyCase_ == BodyOneofCase.OrderTransactionData) {
        output.WriteRawTag(66);
        output.WriteMessage(OrderTransactionData);
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
      if (DataType != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType.Unknown) {
        output.WriteRawTag(8);
        output.WriteEnum((int) DataType);
      }
      if (bodyCase_ == BodyOneofCase.QuoteData) {
        output.WriteRawTag(18);
        output.WriteMessage(QuoteData);
      }
      if (bodyCase_ == BodyOneofCase.QuoteDepthData) {
        output.WriteRawTag(26);
        output.WriteMessage(QuoteDepthData);
      }
      if (bodyCase_ == BodyOneofCase.TradeTickData) {
        output.WriteRawTag(34);
        output.WriteMessage(TradeTickData);
      }
      if (bodyCase_ == BodyOneofCase.PositionData) {
        output.WriteRawTag(42);
        output.WriteMessage(PositionData);
      }
      if (bodyCase_ == BodyOneofCase.AssetData) {
        output.WriteRawTag(50);
        output.WriteMessage(AssetData);
      }
      if (bodyCase_ == BodyOneofCase.OrderStatusData) {
        output.WriteRawTag(58);
        output.WriteMessage(OrderStatusData);
      }
      if (bodyCase_ == BodyOneofCase.OrderTransactionData) {
        output.WriteRawTag(66);
        output.WriteMessage(OrderTransactionData);
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
      if (DataType != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType.Unknown) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) DataType);
      }
      if (bodyCase_ == BodyOneofCase.QuoteData) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(QuoteData);
      }
      if (bodyCase_ == BodyOneofCase.QuoteDepthData) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(QuoteDepthData);
      }
      if (bodyCase_ == BodyOneofCase.TradeTickData) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(TradeTickData);
      }
      if (bodyCase_ == BodyOneofCase.PositionData) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(PositionData);
      }
      if (bodyCase_ == BodyOneofCase.AssetData) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AssetData);
      }
      if (bodyCase_ == BodyOneofCase.OrderStatusData) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(OrderStatusData);
      }
      if (bodyCase_ == BodyOneofCase.OrderTransactionData) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(OrderTransactionData);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(PushData other) {
      if (other == null) {
        return;
      }
      if (other.DataType != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType.Unknown) {
        DataType = other.DataType;
      }
      switch (other.BodyCase) {
        case BodyOneofCase.QuoteData:
          if (QuoteData == null) {
            QuoteData = new global::TigerOpenAPI.Quote.Pb.QuoteData();
          }
          QuoteData.MergeFrom(other.QuoteData);
          break;
        case BodyOneofCase.QuoteDepthData:
          if (QuoteDepthData == null) {
            QuoteDepthData = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData();
          }
          QuoteDepthData.MergeFrom(other.QuoteDepthData);
          break;
        case BodyOneofCase.TradeTickData:
          if (TradeTickData == null) {
            TradeTickData = new global::TigerOpenAPI.Quote.Pb.TradeTickData();
          }
          TradeTickData.MergeFrom(other.TradeTickData);
          break;
        case BodyOneofCase.PositionData:
          if (PositionData == null) {
            PositionData = new global::TigerOpenAPI.Quote.Pb.PositionData();
          }
          PositionData.MergeFrom(other.PositionData);
          break;
        case BodyOneofCase.AssetData:
          if (AssetData == null) {
            AssetData = new global::TigerOpenAPI.Quote.Pb.AssetData();
          }
          AssetData.MergeFrom(other.AssetData);
          break;
        case BodyOneofCase.OrderStatusData:
          if (OrderStatusData == null) {
            OrderStatusData = new global::TigerOpenAPI.Quote.Pb.OrderStatusData();
          }
          OrderStatusData.MergeFrom(other.OrderStatusData);
          break;
        case BodyOneofCase.OrderTransactionData:
          if (OrderTransactionData == null) {
            OrderTransactionData = new global::TigerOpenAPI.Quote.Pb.OrderTransactionData();
          }
          OrderTransactionData.MergeFrom(other.OrderTransactionData);
          break;
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
          case 8: {
            DataType = (global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType) input.ReadEnum();
            break;
          }
          case 18: {
            global::TigerOpenAPI.Quote.Pb.QuoteData subBuilder = new global::TigerOpenAPI.Quote.Pb.QuoteData();
            if (bodyCase_ == BodyOneofCase.QuoteData) {
              subBuilder.MergeFrom(QuoteData);
            }
            input.ReadMessage(subBuilder);
            QuoteData = subBuilder;
            break;
          }
          case 26: {
            global::TigerOpenAPI.Quote.Pb.QuoteDepthData subBuilder = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData();
            if (bodyCase_ == BodyOneofCase.QuoteDepthData) {
              subBuilder.MergeFrom(QuoteDepthData);
            }
            input.ReadMessage(subBuilder);
            QuoteDepthData = subBuilder;
            break;
          }
          case 34: {
            global::TigerOpenAPI.Quote.Pb.TradeTickData subBuilder = new global::TigerOpenAPI.Quote.Pb.TradeTickData();
            if (bodyCase_ == BodyOneofCase.TradeTickData) {
              subBuilder.MergeFrom(TradeTickData);
            }
            input.ReadMessage(subBuilder);
            TradeTickData = subBuilder;
            break;
          }
          case 42: {
            global::TigerOpenAPI.Quote.Pb.PositionData subBuilder = new global::TigerOpenAPI.Quote.Pb.PositionData();
            if (bodyCase_ == BodyOneofCase.PositionData) {
              subBuilder.MergeFrom(PositionData);
            }
            input.ReadMessage(subBuilder);
            PositionData = subBuilder;
            break;
          }
          case 50: {
            global::TigerOpenAPI.Quote.Pb.AssetData subBuilder = new global::TigerOpenAPI.Quote.Pb.AssetData();
            if (bodyCase_ == BodyOneofCase.AssetData) {
              subBuilder.MergeFrom(AssetData);
            }
            input.ReadMessage(subBuilder);
            AssetData = subBuilder;
            break;
          }
          case 58: {
            global::TigerOpenAPI.Quote.Pb.OrderStatusData subBuilder = new global::TigerOpenAPI.Quote.Pb.OrderStatusData();
            if (bodyCase_ == BodyOneofCase.OrderStatusData) {
              subBuilder.MergeFrom(OrderStatusData);
            }
            input.ReadMessage(subBuilder);
            OrderStatusData = subBuilder;
            break;
          }
          case 66: {
            global::TigerOpenAPI.Quote.Pb.OrderTransactionData subBuilder = new global::TigerOpenAPI.Quote.Pb.OrderTransactionData();
            if (bodyCase_ == BodyOneofCase.OrderTransactionData) {
              subBuilder.MergeFrom(OrderTransactionData);
            }
            input.ReadMessage(subBuilder);
            OrderTransactionData = subBuilder;
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
          case 8: {
            DataType = (global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.DataType) input.ReadEnum();
            break;
          }
          case 18: {
            global::TigerOpenAPI.Quote.Pb.QuoteData subBuilder = new global::TigerOpenAPI.Quote.Pb.QuoteData();
            if (bodyCase_ == BodyOneofCase.QuoteData) {
              subBuilder.MergeFrom(QuoteData);
            }
            input.ReadMessage(subBuilder);
            QuoteData = subBuilder;
            break;
          }
          case 26: {
            global::TigerOpenAPI.Quote.Pb.QuoteDepthData subBuilder = new global::TigerOpenAPI.Quote.Pb.QuoteDepthData();
            if (bodyCase_ == BodyOneofCase.QuoteDepthData) {
              subBuilder.MergeFrom(QuoteDepthData);
            }
            input.ReadMessage(subBuilder);
            QuoteDepthData = subBuilder;
            break;
          }
          case 34: {
            global::TigerOpenAPI.Quote.Pb.TradeTickData subBuilder = new global::TigerOpenAPI.Quote.Pb.TradeTickData();
            if (bodyCase_ == BodyOneofCase.TradeTickData) {
              subBuilder.MergeFrom(TradeTickData);
            }
            input.ReadMessage(subBuilder);
            TradeTickData = subBuilder;
            break;
          }
          case 42: {
            global::TigerOpenAPI.Quote.Pb.PositionData subBuilder = new global::TigerOpenAPI.Quote.Pb.PositionData();
            if (bodyCase_ == BodyOneofCase.PositionData) {
              subBuilder.MergeFrom(PositionData);
            }
            input.ReadMessage(subBuilder);
            PositionData = subBuilder;
            break;
          }
          case 50: {
            global::TigerOpenAPI.Quote.Pb.AssetData subBuilder = new global::TigerOpenAPI.Quote.Pb.AssetData();
            if (bodyCase_ == BodyOneofCase.AssetData) {
              subBuilder.MergeFrom(AssetData);
            }
            input.ReadMessage(subBuilder);
            AssetData = subBuilder;
            break;
          }
          case 58: {
            global::TigerOpenAPI.Quote.Pb.OrderStatusData subBuilder = new global::TigerOpenAPI.Quote.Pb.OrderStatusData();
            if (bodyCase_ == BodyOneofCase.OrderStatusData) {
              subBuilder.MergeFrom(OrderStatusData);
            }
            input.ReadMessage(subBuilder);
            OrderStatusData = subBuilder;
            break;
          }
          case 66: {
            global::TigerOpenAPI.Quote.Pb.OrderTransactionData subBuilder = new global::TigerOpenAPI.Quote.Pb.OrderTransactionData();
            if (bodyCase_ == BodyOneofCase.OrderTransactionData) {
              subBuilder.MergeFrom(OrderTransactionData);
            }
            input.ReadMessage(subBuilder);
            OrderTransactionData = subBuilder;
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code