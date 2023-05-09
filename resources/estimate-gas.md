### Reference

1. [Base knowledge](#base_knowledge)
2. [Priority fee estimation](#priority_fee_estimation)
   1. [Building for production](#building_for_production)
3. [Gas limit estimation](#gas-limit-estimation)
4. [Reference](#reference)

---

### Base knowledge <a name="base_knowledge"></a>

After the London upgrade, the total fee would now be: `unit of gas used * (base fee + priority fee)` where the `base fee` is a value set by the protocol and the `priority fee` is a value set by the users as a tip to the validator. Moreover, `unit of gas used` is the maximum number of units of gas you are willing to pay for in order to carry out a transaction or EVM operation. Obviously, the `unit of gas used` is proportional to the size of `dataCall`. The transaction is more complex, the `dataCall` is larger and `unit of gas used` is more. If you want to learn more to gas and price, you can read this [topic](./gas-and-price.md).

As you can see above, except the `base fee` is decided by the protocol, the `unit of gas used`(or you can known as `gas limit`) is based on your transaction that you want to send to network and the `priority fee` base on user decision completely. Hence, the problem we must resolve is how to estimate `gas limit` and `priority fee` effectively.

### Priority fee estimation <a name="priority_fee_estimation"></a>

The easiest way to estimate priority fee is use `eth_feeHistory`. Here is a call the `eth_feeHistory` and it's result:

```shell
const historicalBlocks = 4;
web3.eth.getFeeHistory(historicalBlocks, "pending", [25, 50, 75]).then(console.log);
```

The above call means, "Give me the fee history information starting from the pending block and looking backward 4 blocks. For each block also give me the 25th, 50th, and 75th percentiles of priority fees for transactions in the block". The raw result looks like this:

```shell
{
  oldestBlock: 12966149,
  reward: [
    [ '0x6fb9cbef7', '0x161dea08f7', '0x28be4928f7' ],
    [ '0x10378b36e1', '0x1bdbc6aae1', '0x20fb1406e1' ],
    [ '0x112fcac6f6', '0x1dfe0c2cf6', '0x287841aef6' ],
    [ '0x1dfc552db', '0x59971f2db', '0xd8400c6db' ]
  ],
  baseFeePerGas: [
    '0x1d1b1b8f09',
    '0x1e5962991f',
    '0x1d6123090a',
    '0x210ced0925',
    '0x252e208712'
  ],
  gasUsedRatio: [
    0.6708618436856891,
    0.3721915376646354,
    0.9998122039490063,
    0.9998039574899923
  ]
}
```

Or you can use some simple code to format this result to become below:

```shell
[
  {
    number: 12966164,
    baseFeePerGas: 315777006840,
    gasUsedRatio: 0.9922326809477219,
    priorityFeePerGas: [ 34222993160, 34222993160, 63222993160 ]
  },
  {
    number: 12966165,
    baseFeePerGas: 354635947504,
    gasUsedRatio: 0.22772779167798343,
    priorityFeePerGas: [ 20000000000, 38044555767, 38364052496 ]
  },
  {
    number: 12966166,
    baseFeePerGas: 330496570085,
    gasUsedRatio: 0.8876034775653597,
    priorityFeePerGas: [ 9503429915, 19503429915, 36503429915 ]
  },
  {
    number: 12966167,
    baseFeePerGas: 362521975057,
    gasUsedRatio: 0.9909446241177369,
    priorityFeePerGas: [ 18478024943, 37478024943, 81478024943 ]
  }
]
```

Which is much more readable. We asked for 4 blocks, so each element of the array is a single block. The 3 elements in the `priorityFeePerGas` field represent the 25th, 50th, and 75th percentiles of priority fees that were submitted in those blocks.

When creating as estimate we need to determine (a) <a name="pge_a"></a> how far back in time to look and (b) <a name="pge_b"></a> what percentiles of priority fees to look at. After determining two problem [(a)](#pge_a) and [(b)](#pge_b), we have a list historical gas's data list. Simply, we can calculate the average of historical priority gas and consider it as estimated priority fee.

What if we want to have many options, such as three options: slow, average and fast. We can accomplish this by providing our eth_feeHistory call with 3 percentiles. Since we are doing the 1st percentile on the "slow" end, let's do the 99th percentile on the "fast" end, and of course "average" refers to the 50th percentile.

#### Building for production <a name="building_for_production"></a>

Well, that was fun! But this code would not be particularly viable in production. Running these calculations every time we need an estimate is not practical for an application that might be serving thousands of transactions per second.

Geth consults what's known as an "Oracle"(this is an [example](https://github.com/peppersec/gas-price-oracle)) where a software actor whose only job is to keep track of historical blocks and keep the gas estimates up to date. Then Geth will simply ask the oracle "what is the current estimate" and get back an immediate response. If you plan on building your own estimator, we suggest you do the same.

### Gas limit estimation <a name="gas_limit_estimation"></a>

Two main solution for estimate gas limit is: using `estimateGas` RPC or building a simulation for simulate the transaction operation with particular state to determine a effective gas limit. In particular, two solution is same, both is based on the idea that run the transaction in test environment to measure gas limit need to run and consider it is estimated gas limit.

### Reference <a name="reference"></a>

- https://docs.alchemy.com/docs/how-to-build-a-gas-fee-estimator-using-eip-1559
- https://github.com/peppersec/gas-price-oracle
