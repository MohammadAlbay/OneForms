namespace Modetor.Design.UI
{
    public enum IconSet : int
    {
        Wifi = 0xE701, Bluetooth = 0xE702, Connect = 0xE703,
        InternetSharing = 0xE704, Brightness = 0xE706,
        Airplane = 0xE709, Setting = 0xE713, Main = 0xE715,
        Filter = 0xE71C, Mic = 0xE720, Lock = 0xE72E,
        ReportHacked = 0xE730, FavoriteStar = 0xE735,
        CheckboxComposite = 0xE73A, CheckboxCompositeReversed = 0xE73D,
        Delete = 0xE74D, Save = 0xE74E, Mute = 0xE74F, Cloud = 0xE753,
        CommandPrompt = 0xE756, Multiselect = 0xE762, KeyboardClassic = 0xE765,
        Volume = 0xE767, Play = 0xE768, Pause = 0xE769, Smail = 0xE76E,
        Glob = 0xE774, UpdateRestore = 0xE777, Contact = 0xE77B,
        Errpr = 0xE783, Unlock = 0xE785, PowerButton = 0xE7E8,
        Tooltip = 0xE82F, FolderOpen = 0xE838, DirectAccess = 0xE83B,
        WifiHotspot = 0xE88A, Usb = 0xE88E, Clear = 0xE894, Sync = 0xE895,
        Download = 0xE896, Next = 0xE893, Previous = 0xE892, Help = 0xE897,
        Upload = 0xE898, Message = 0xE8BD, MailRead = 0xE8C3, Important = 0xE8C9,
        Contact2 = 0xE8D4, Audio = 0xE8D6, Permission = 0xE8D7, BlockedContact = 0xE8F8,
        Accept = 0xE8FB, Scanner = 0xE8FE, Account = 0xE910, Completed = 0xE930,
        Fingerprint = 0xE928, health = 0xE95E, Mouse = 0xE962, RightDoubleQuote = 0xE9B1,
        LeftDoubleQuote = 0xE9B2, Diagnostic = 0xE9D9, Shield = 0xEA18,
        HeartBroken = 0xEA92, Like = 0xE8E1, Dislike = 0xE8E0, Heart = 0xEB51,
        HeartFill = 0xEB52, WifiError = 0xEB5E, NUIFace = 0xEB68,
        Datetime = 0xEC92, AddTo = 0xECC8, RemoveFrom = 0xECC9,
        CloudSearch = 0xEDE4,

    }

    public static class IconSetConvert
    {
        public static string Get(IconSet icon) => ((char)icon) + "";
    }
}
