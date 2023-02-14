// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Response.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace TigerOpenAPI.Quote.Pb {

  /// <summary>Holder for reflection information generated from Response.proto</summary>
  public static partial class ResponseReflection {

    #region Descriptor
    /// <summary>File descriptor for Response.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ResponseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg5SZXNwb25zZS5wcm90bxI0Y29tLnRpZ2VyYnJva2Vycy5zdG9jay5vcGVu",
            "YXBpLmNsaWVudC5zb2NrZXQuZGF0YS5wYhoSU29ja2V0Q29tbW9uLnByb3Rv",
            "Gg5QdXNoRGF0YS5wcm90byKRAgoIUmVzcG9uc2USWwoHY29tbWFuZBgBIAEo",
            "DjJKLmNvbS50aWdlcmJyb2tlcnMuc3RvY2sub3BlbmFwaS5jbGllbnQuc29j",
            "a2V0LmRhdGEucGIuU29ja2V0Q29tbW9uLkNvbW1hbmQSDwoCaWQYAiABKA1I",
            "AIgBARIRCgRjb2RlGAMgASgFSAGIAQESEAoDbXNnGAQgASgJSAKIAQESUQoE",
            "Ym9keRgFIAEoCzI+LmNvbS50aWdlcmJyb2tlcnMuc3RvY2sub3BlbmFwaS5j",
            "bGllbnQuc29ja2V0LmRhdGEucGIuUHVzaERhdGFIA4gBAUIFCgNfaWRCBwoF",
            "X2NvZGVCBgoEX21zZ0IHCgVfYm9keUIYqgIVVGlnZXJPcGVuQVBJLlF1b3Rl",
            "LlBiUABQAWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::TigerOpenAPI.Quote.Pb.SocketCommonReflection.Descriptor, global::TigerOpenAPI.Quote.Pb.PushDataReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::TigerOpenAPI.Quote.Pb.Response), global::TigerOpenAPI.Quote.Pb.Response.Parser, new[]{ "Command", "Id", "Code", "Msg", "Body" }, new[]{ "Id", "Code", "Msg", "Body" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Response : pb::IMessage<Response>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Response> _parser = new pb::MessageParser<Response>(() => new Response());
    private pb::UnknownFieldSet _unknownFields;
    private int _hasBits0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Response> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::TigerOpenAPI.Quote.Pb.ResponseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Response() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Response(Response other) : this() {
      _hasBits0 = other._hasBits0;
      command_ = other.command_;
      id_ = other.id_;
      code_ = other.code_;
      msg_ = other.msg_;
      body_ = other.body_ != null ? other.body_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Response Clone() {
      return new Response(this);
    }

    /// <summary>Field number for the "command" field.</summary>
    public const int CommandFieldNumber = 1;
    private global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command command_ = global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command.Unknown;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command Command {
      get { return command_; }
      set {
        command_ = value;
      }
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 2;
    private uint id_;
    /// <summary>
    /// from request's id
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint Id {
      get { if ((_hasBits0 & 1) != 0) { return id_; } else { return 0; } }
      set {
        _hasBits0 |= 1;
        id_ = value;
      }
    }
    /// <summary>Gets whether the "id" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasId {
      get { return (_hasBits0 & 1) != 0; }
    }
    /// <summary>Clears the value of the "id" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearId() {
      _hasBits0 &= ~1;
    }

    /// <summary>Field number for the "code" field.</summary>
    public const int CodeFieldNumber = 3;
    private int code_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int Code {
      get { if ((_hasBits0 & 2) != 0) { return code_; } else { return 0; } }
      set {
        _hasBits0 |= 2;
        code_ = value;
      }
    }
    /// <summary>Gets whether the "code" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasCode {
      get { return (_hasBits0 & 2) != 0; }
    }
    /// <summary>Clears the value of the "code" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearCode() {
      _hasBits0 &= ~2;
    }

    /// <summary>Field number for the "msg" field.</summary>
    public const int MsgFieldNumber = 4;
    private string msg_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Msg {
      get { return msg_ ?? ""; }
      set {
        msg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }
    /// <summary>Gets whether the "msg" field is set</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasMsg {
      get { return msg_ != null; }
    }
    /// <summary>Clears the value of the "msg" field</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearMsg() {
      msg_ = null;
    }

    /// <summary>Field number for the "body" field.</summary>
    public const int BodyFieldNumber = 5;
    private global::TigerOpenAPI.Quote.Pb.PushData body_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::TigerOpenAPI.Quote.Pb.PushData Body {
      get { return body_; }
      set {
        body_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Response);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Response other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Command != other.Command) return false;
      if (Id != other.Id) return false;
      if (Code != other.Code) return false;
      if (Msg != other.Msg) return false;
      if (!object.Equals(Body, other.Body)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Command != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command.Unknown) hash ^= Command.GetHashCode();
      if (HasId) hash ^= Id.GetHashCode();
      if (HasCode) hash ^= Code.GetHashCode();
      if (HasMsg) hash ^= Msg.GetHashCode();
      if (body_ != null) hash ^= Body.GetHashCode();
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
      if (Command != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command.Unknown) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Command);
      }
      if (HasId) {
        output.WriteRawTag(16);
        output.WriteUInt32(Id);
      }
      if (HasCode) {
        output.WriteRawTag(24);
        output.WriteInt32(Code);
      }
      if (HasMsg) {
        output.WriteRawTag(34);
        output.WriteString(Msg);
      }
      if (body_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(Body);
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
      if (Command != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command.Unknown) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Command);
      }
      if (HasId) {
        output.WriteRawTag(16);
        output.WriteUInt32(Id);
      }
      if (HasCode) {
        output.WriteRawTag(24);
        output.WriteInt32(Code);
      }
      if (HasMsg) {
        output.WriteRawTag(34);
        output.WriteString(Msg);
      }
      if (body_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(Body);
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
      if (Command != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command.Unknown) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Command);
      }
      if (HasId) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Id);
      }
      if (HasCode) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Code);
      }
      if (HasMsg) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Msg);
      }
      if (body_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Body);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Response other) {
      if (other == null) {
        return;
      }
      if (other.Command != global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command.Unknown) {
        Command = other.Command;
      }
      if (other.HasId) {
        Id = other.Id;
      }
      if (other.HasCode) {
        Code = other.Code;
      }
      if (other.HasMsg) {
        Msg = other.Msg;
      }
      if (other.body_ != null) {
        if (body_ == null) {
          Body = new global::TigerOpenAPI.Quote.Pb.PushData();
        }
        Body.MergeFrom(other.Body);
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
            Command = (global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command) input.ReadEnum();
            break;
          }
          case 16: {
            Id = input.ReadUInt32();
            break;
          }
          case 24: {
            Code = input.ReadInt32();
            break;
          }
          case 34: {
            Msg = input.ReadString();
            break;
          }
          case 42: {
            if (body_ == null) {
              Body = new global::TigerOpenAPI.Quote.Pb.PushData();
            }
            input.ReadMessage(Body);
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
            Command = (global::TigerOpenAPI.Quote.Pb.SocketCommon.Types.Command) input.ReadEnum();
            break;
          }
          case 16: {
            Id = input.ReadUInt32();
            break;
          }
          case 24: {
            Code = input.ReadInt32();
            break;
          }
          case 34: {
            Msg = input.ReadString();
            break;
          }
          case 42: {
            if (body_ == null) {
              Body = new global::TigerOpenAPI.Quote.Pb.PushData();
            }
            input.ReadMessage(Body);
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