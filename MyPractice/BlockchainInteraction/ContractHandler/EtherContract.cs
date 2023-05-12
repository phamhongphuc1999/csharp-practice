namespace BlockchainInteraction.ContractHandler
{
  public class EtherContract
  {
    public string address;
    protected ContractInterface inter;
    public string rpcUrl;
    public EtherQuery provider;

    public EtherContract(string address, string abiFile, string provider)
    {
      this.address = address;
      this.inter = new ContractInterface(abiFile);
      this.rpcUrl = provider;
      this.provider = new EtherQuery(provider);
    }

    public EtherContract(string address, string abiFile, EtherQuery provider)
    {
      this.address = address;
      this.inter = new ContractInterface(abiFile);
      this.rpcUrl = provider.rpcUrl;
      this.provider = provider;
    }

    public EtherContract(string address, ContractInterface inter, string provider)
    {
      this.address = address;
      this.inter = inter;
      this.rpcUrl = provider;
      this.provider = new EtherQuery(provider);
    }

    public EtherContract(string address, ContractInterface inter, EtherQuery provider)
    {
      this.address = address;
      this.inter = inter;
      this.rpcUrl = provider.rpcUrl;
      this.provider = provider;
    }
  }
}
