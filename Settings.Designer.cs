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
        public string AlertSound {
            get {
                return ((string)(this["AlertSound"]));
            }
            set {
                this["AlertSound"] = value;
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
        public int NotificationFormWidth {
            get {
                return ((int)(this["NotificationFormWidth"]));
            }
            set {
                this["NotificationFormWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("310")]
        public int NotificationFormHeight {
            get {
                return ((int)(this["NotificationFormHeight"]));
            }
            set {
                this["NotificationFormHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public int NotificationFormRightPosition {
            get {
                return ((int)(this["NotificationFormRightPosition"]));
            }
            set {
                this["NotificationFormRightPosition"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int AlertFormWidth {
            get {
                return ((int)(this["AlertFormWidth"]));
            }
            set {
                this["AlertFormWidth"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int AlertFormHeight {
            get {
                return ((int)(this["AlertFormHeight"]));
            }
            set {
                this["AlertFormHeight"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("50")]
        public int AlertFormRightPosition {
            get {
                return ((int)(this["AlertFormRightPosition"]));
            }
            set {
                this["AlertFormRightPosition"] = value;
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
        public int InfoWindowLifeTimeInSecs {
            get {
                return ((int)(this["InfoWindowLifeTimeInSecs"]));
            }
            set {
                this["InfoWindowLifeTimeInSecs"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0, 50")]
        public global::System.Drawing.Point InfoWindowRightBottomPosition {
            get {
                return ((global::System.Drawing.Point)(this["InfoWindowRightBottomPosition"]));
            }
            set {
                this["InfoWindowRightBottomPosition"] = value;
            }
        }
    }
}
