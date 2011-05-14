﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3074.
// 
#pragma warning disable 1591

namespace RISVoiceControl.RISServer {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="AudioUploadSoap", Namespace="http://tempuri.org/")]
    public partial class AudioUpload : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HelloWorldOperationCompleted;
        
        private System.Threading.SendOrPostCallback UploadCompleteFileOperationCompleted;
        
        private System.Threading.SendOrPostCallback UploadFileOperationCompleted;
        
        private System.Threading.SendOrPostCallback IsFindingPresentOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFileSizeOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetCompleteFileOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetChunkSizeOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public AudioUpload() {
            this.Url = global::RISVoiceControl.Properties.Settings.Default.RISVoiceControl_RISServer_AudioUpload;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event HelloWorldCompletedEventHandler HelloWorldCompleted;
        
        /// <remarks/>
        public event UploadCompleteFileCompletedEventHandler UploadCompleteFileCompleted;
        
        /// <remarks/>
        public event UploadFileCompletedEventHandler UploadFileCompleted;
        
        /// <remarks/>
        public event IsFindingPresentCompletedEventHandler IsFindingPresentCompleted;
        
        /// <remarks/>
        public event GetFileSizeCompletedEventHandler GetFileSizeCompleted;
        
        /// <remarks/>
        public event GetCompleteFileCompletedEventHandler GetCompleteFileCompleted;
        
        /// <remarks/>
        public event GetChunkSizeCompletedEventHandler GetChunkSizeCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HelloWorld", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HelloWorld() {
            object[] results = this.Invoke("HelloWorld", new object[0]);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HelloWorldAsync() {
            this.HelloWorldAsync(null);
        }
        
        /// <remarks/>
        public void HelloWorldAsync(object userState) {
            if ((this.HelloWorldOperationCompleted == null)) {
                this.HelloWorldOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHelloWorldOperationCompleted);
            }
            this.InvokeAsync("HelloWorld", new object[0], this.HelloWorldOperationCompleted, userState);
        }
        
        private void OnHelloWorldOperationCompleted(object arg) {
            if ((this.HelloWorldCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HelloWorldCompleted(this, new HelloWorldCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UploadCompleteFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public object UploadCompleteFile([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] buffer, string fileName, int studyId, int radiologistId, int findingId) {
            object[] results = this.Invoke("UploadCompleteFile", new object[] {
                        buffer,
                        fileName,
                        studyId,
                        radiologistId,
                        findingId});
            return ((object)(results[0]));
        }
        
        /// <remarks/>
        public void UploadCompleteFileAsync(byte[] buffer, string fileName, int studyId, int radiologistId, int findingId) {
            this.UploadCompleteFileAsync(buffer, fileName, studyId, radiologistId, findingId, null);
        }
        
        /// <remarks/>
        public void UploadCompleteFileAsync(byte[] buffer, string fileName, int studyId, int radiologistId, int findingId, object userState) {
            if ((this.UploadCompleteFileOperationCompleted == null)) {
                this.UploadCompleteFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadCompleteFileOperationCompleted);
            }
            this.InvokeAsync("UploadCompleteFile", new object[] {
                        buffer,
                        fileName,
                        studyId,
                        radiologistId,
                        findingId}, this.UploadCompleteFileOperationCompleted, userState);
        }
        
        private void OnUploadCompleteFileOperationCompleted(object arg) {
            if ((this.UploadCompleteFileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadCompleteFileCompleted(this, new UploadCompleteFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UploadFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int UploadFile([System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")] byte[] buffer, string fileName, int studyId, int radiologistId, int findingId, bool isEnd, bool isStart) {
            object[] results = this.Invoke("UploadFile", new object[] {
                        buffer,
                        fileName,
                        studyId,
                        radiologistId,
                        findingId,
                        isEnd,
                        isStart});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void UploadFileAsync(byte[] buffer, string fileName, int studyId, int radiologistId, int findingId, bool isEnd, bool isStart) {
            this.UploadFileAsync(buffer, fileName, studyId, radiologistId, findingId, isEnd, isStart, null);
        }
        
        /// <remarks/>
        public void UploadFileAsync(byte[] buffer, string fileName, int studyId, int radiologistId, int findingId, bool isEnd, bool isStart, object userState) {
            if ((this.UploadFileOperationCompleted == null)) {
                this.UploadFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUploadFileOperationCompleted);
            }
            this.InvokeAsync("UploadFile", new object[] {
                        buffer,
                        fileName,
                        studyId,
                        radiologistId,
                        findingId,
                        isEnd,
                        isStart}, this.UploadFileOperationCompleted, userState);
        }
        
        private void OnUploadFileOperationCompleted(object arg) {
            if ((this.UploadFileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UploadFileCompleted(this, new UploadFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IsFindingPresent", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool IsFindingPresent(int findingId, int radiologistId) {
            object[] results = this.Invoke("IsFindingPresent", new object[] {
                        findingId,
                        radiologistId});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void IsFindingPresentAsync(int findingId, int radiologistId) {
            this.IsFindingPresentAsync(findingId, radiologistId, null);
        }
        
        /// <remarks/>
        public void IsFindingPresentAsync(int findingId, int radiologistId, object userState) {
            if ((this.IsFindingPresentOperationCompleted == null)) {
                this.IsFindingPresentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIsFindingPresentOperationCompleted);
            }
            this.InvokeAsync("IsFindingPresent", new object[] {
                        findingId,
                        radiologistId}, this.IsFindingPresentOperationCompleted, userState);
        }
        
        private void OnIsFindingPresentOperationCompleted(object arg) {
            if ((this.IsFindingPresentCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IsFindingPresentCompleted(this, new IsFindingPresentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFileSize", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long GetFileSize(int findingId, int radiologistId) {
            object[] results = this.Invoke("GetFileSize", new object[] {
                        findingId,
                        radiologistId});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void GetFileSizeAsync(int findingId, int radiologistId) {
            this.GetFileSizeAsync(findingId, radiologistId, null);
        }
        
        /// <remarks/>
        public void GetFileSizeAsync(int findingId, int radiologistId, object userState) {
            if ((this.GetFileSizeOperationCompleted == null)) {
                this.GetFileSizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFileSizeOperationCompleted);
            }
            this.InvokeAsync("GetFileSize", new object[] {
                        findingId,
                        radiologistId}, this.GetFileSizeOperationCompleted, userState);
        }
        
        private void OnGetFileSizeOperationCompleted(object arg) {
            if ((this.GetFileSizeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFileSizeCompleted(this, new GetFileSizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCompleteFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetCompleteFile(int findingId, int radiologistId) {
            object[] results = this.Invoke("GetCompleteFile", new object[] {
                        findingId,
                        radiologistId});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GetCompleteFileAsync(int findingId, int radiologistId) {
            this.GetCompleteFileAsync(findingId, radiologistId, null);
        }
        
        /// <remarks/>
        public void GetCompleteFileAsync(int findingId, int radiologistId, object userState) {
            if ((this.GetCompleteFileOperationCompleted == null)) {
                this.GetCompleteFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetCompleteFileOperationCompleted);
            }
            this.InvokeAsync("GetCompleteFile", new object[] {
                        findingId,
                        radiologistId}, this.GetCompleteFileOperationCompleted, userState);
        }
        
        private void OnGetCompleteFileOperationCompleted(object arg) {
            if ((this.GetCompleteFileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetCompleteFileCompleted(this, new GetCompleteFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetChunkSize", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int GetChunkSize() {
            object[] results = this.Invoke("GetChunkSize", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void GetChunkSizeAsync() {
            this.GetChunkSizeAsync(null);
        }
        
        /// <remarks/>
        public void GetChunkSizeAsync(object userState) {
            if ((this.GetChunkSizeOperationCompleted == null)) {
                this.GetChunkSizeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetChunkSizeOperationCompleted);
            }
            this.InvokeAsync("GetChunkSize", new object[0], this.GetChunkSizeOperationCompleted, userState);
        }
        
        private void OnGetChunkSizeOperationCompleted(object arg) {
            if ((this.GetChunkSizeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetChunkSizeCompleted(this, new GetChunkSizeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void HelloWorldCompletedEventHandler(object sender, HelloWorldCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HelloWorldCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HelloWorldCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void UploadCompleteFileCompletedEventHandler(object sender, UploadCompleteFileCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadCompleteFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UploadCompleteFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public object Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((object)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void UploadFileCompletedEventHandler(object sender, UploadFileCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UploadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UploadFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void IsFindingPresentCompletedEventHandler(object sender, IsFindingPresentCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IsFindingPresentCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IsFindingPresentCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetFileSizeCompletedEventHandler(object sender, GetFileSizeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFileSizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFileSizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetCompleteFileCompletedEventHandler(object sender, GetCompleteFileCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetCompleteFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetCompleteFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetChunkSizeCompletedEventHandler(object sender, GetChunkSizeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetChunkSizeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetChunkSizeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591