### Reference

1. [What is gas?](#what_is_gas)
2. [Block size](#block_size)
3. [Base fee](#base_fee)
4. [Priority fee(tips)](#priority_fee)
5. [Max fee](#max_fee)
6. [Gas limit](#gas_limit)
7. [Reference](#reference)

---

### What is gas <a name="what_is_gas"></a>

Gas is the unit that measures the amount of computational effort required to execute specific operations on the Ethereum network. Gas always is required to execute a transaction in Ethereum, regardless of success or failure.

Gas fees are paid in Ethereum's native currency, ether(ETH). Gas price are denoted in gwei, which itself denomination of ETH - each gwei equal to 0.000000001ETH.

After the London upgrade, the total fee would now be: `unit of gas used * (base fee + priority fee)` where the `base fee` is a value set by the protocol and the `priority fee` is a value set by the users as a tip to the validator.

We have a example: Jordan has to pay to Taylor 1ETH. In the transaction, the gas limit is 21000 units, the base fee is 10 gwei, Jordan includes a tip of 2 gwei. Base on above assumption, the Jordan total fee is: 21000 \* (10 + 2) = 252000 gwei or 0.000252 ETH. When Jordan sends the money, 1.000252 ETH will be deducted from Jordan's account. Taylor will be credited 1 ETH. Validator receive the tip of 0.000042 ETH. Base fee of 0.00021 ETH is burned.

### Block size <a name="block_size"></a>

Before the London Upgrade, Ethereum had fixed-sized blocks. In time of high network demand, these blocks operated at full capacity. As a result, users often had to wait for demand to reduce to get included in a block, which led to a poor user experience.

The London Upgrade introduced variable-sized blocks to Ethereum. Each block has a target size of 15 million gas, but the size blocks will increase or decrease in accordance with network demand, up until the block limit of 30 million gas(2x the target block size). Similarly, the protocol will decrease the base fee if the block size is less then the target block size. The amount by which the base fee is adjusted is proportional to how far the current block size is from the target.

### Base fee <a name="base_fee"></a>

The `Base Fee` is determined by the Ethereum network rather than being set by end-users looking to transact or miners seeking to validate transactions. The `Base Fee` targets 50% full blocks and is based upon the contents of the most recent confirmed block. Depending on how full that new block is, the Base Fee is automatically increased or decreased. For example:

- If the last block was exactly 50% full, the `Base Fee` will remain unchanged.
- If the last block was 100% full, the `Base Fee` will increase by the maximum 12.5% for the next block.
- If the last block was more than 50% full but less than 100% full, the `Base Fee` will increase by less than 12.5%.
- If the last block was 0% full – that is, empty – the `Base fee` will decrease the maximum 12.5% for the next block.
- If the last block was more than 0% full but less than 50% full, the `Base Fee` will decrease by less than 12.5%

### Priority fee(tips) <a name="priority_fee"></a>

Before the London Upgrade, miners would receive total gas fee from any transaction included in a block. With the new base fee getting burned, the London Upgrade introduced a priority fee to, known as the `maxPriorityFeePerGas`, incentivize miners to include a transaction in the block. Without tips, miners would find it economically viable to mine empty blocks, as they would receive the same block reward. Under normal conditions, a small tip provides miners a minimal incentive to include a transaction. For transactions that need to get preferentially executed ahead of other transactions in the same block, a higher tip will be necessary to attempt to outbid competing transactions.

### Max fee <a name="max_fee"></a>

To execute a transaction on the network, users can specify a maximum limit they are willing to pay for their transaction to be executed. This optional parameter is known as the `maxFeePerGas`. For a transaction to be executed, the max fee must exceed the sum of the `base fee` and the `priority fee`. The transaction sender is refunded the difference between the `max fee` and the sum of the `base fee` and the `priority fee`.

### Gas limit <a name="gas_limit"></a>

Gas limit is the maximum number of units of gas you are willing to pay for in order to carry out a transaction or EVM operation. In the formulate: `unit of gas used * (base fee + priority fee)`, you can understand that gas limit is just `unit of gas used`.

### Reference <a name="reference"></a>

- https://ethereum.org/en/developers/docs/gas/
- https://www.blocknative.com/blog/eip-1559-fees
- https://www.notonlyowner.com/learn/what-happens-when-you-send-one-dai
- https://docs.alchemy.com/reference/eth-accounts
