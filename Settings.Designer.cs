﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cliver.CisteraNotification {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2222")]
        public ushort ServicePort {
            get {
                return ((ushort)(this["ServicePort"]));
            }
            set {
                this["ServicePort"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("cliversoft")]
        public string ServiceName {
            get {
                return ((string)(this["ServiceName"]));
            }
            set {
                this["ServiceName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseWindowsUserAsServiceName {
            get {
                return ((bool)(this["UseWindowsUserAsServiceName"]));
            }
            set {
                this["UseWindowsUserAsServiceName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool Run {
            get {
                return ((bool)(this["Run"]));
            }
            set {
                this["Run"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("alert.wav")]
        public string AlertSoundFile {
            get {
                return ((string)(this["AlertSoundFile"]));
            }
            set {
                this["AlertSoundFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("inform.wav")]
        public string InfoSoundFile {
            get {
                return ((string)(this["InfoSoundFile"]));
            }
            set {
                this["InfoSoundFile"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int AlertToastTop {
            get {
                return ((int)(this["AlertToastTop"]));
            }
            set {
                this["AlertToastTop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public int AlertToastRight {
            get {
                return ((int)(this["AlertToastRight"]));
            }
            set {
                this["AlertToastRight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AudioDeviceName {
            get {
                return ((string)(this["AudioDeviceName"]));
            }
            set {
                this["AudioDeviceName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("15")]
        public int InfoToastLifeTimeInSecs {
            get {
                return ((int)(this["InfoToastLifeTimeInSecs"]));
            }
            set {
                this["InfoToastLifeTimeInSecs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int InfoToastRight {
            get {
                return ((int)(this["InfoToastRight"]));
            }
            set {
                this["InfoToastRight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public int InfoToastBottom {
            get {
                return ((int)(this["InfoToastBottom"]));
            }
            set {
                this["InfoToastBottom"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string RtpStreamStorageFolder {
            get {
                return ((string)(this["RtpStreamStorageFolder"]));
            }
            set {
                this["RtpStreamStorageFolder"] = value;
            }
        }
    }
}
