namespace BlockchainInteraction
{
  public static class Constant
  {
    public const string JsonRpcVersion2 = "2.0";

    public static readonly string[] RETRIABLE_ERRORS = new string[] { "Gateway timeout", "ETIMEDOUT", "failed to parse response body", "Failed to fetch" };

    public static class BlockTag
    {
      public const string Earliest = "earliest";
      public const string Finalized = "finalized";
      public const string Safe = "safe";
      public const string Latest = "latest";
      public const string Pending = "pending";
    }
  }
}
