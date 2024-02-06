namespace WorldBoxAPI.Constants {
    internal static class Version {
        public static string ASSEMBLY_VERSION { get; } = "0.1.0";
        public static bool OUTDATED_VERSION { get; } = TARGET_VERSION_CODE != Config.versionCodeText;
        public static string TARGET_VERSION_CODE { get; } = "558";
    }
}
