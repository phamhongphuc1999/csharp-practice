### Reference

1. [Overview](#overview)
2. [eth_estimateUserOperationGas](#eth_estimateUserOperationGas)

### Overview <a name="overview"></a>

You should read [ERC 4337](./erc-4337.md) and [gas and price](../gas-and-price.md) to have the sense to overview. In this topic, we will concern about gas and price in account abstraction. Because of different structure, gas and price in account abstraction differ in other transactions. The below table lists `UserOperation` parameters related gas and price

| Field                | Type    | Description                                                                                        |
| :------------------- | :------ | :------------------------------------------------------------------------------------------------- |
| callGasLimit         | uint256 | The amount of gas to allocate the main execution call                                              |
| verificationGasLimit | uint256 | The amount of gas to allocate for the verification step                                            |
| preVerificationGas   | uint256 | The amount of gas to pay for to compensate the bundler for pre-verification execution and calldata |
| maxFeePerGas         | uint256 | Maximum fee per gas(similar to EIP-1559 max_fee_per_gas)                                           |
| maxPriorityFeePerGas | uint256 | Maximum priority fee per gas                                                                       |

### eth_estimateUserOperationGas <a name="eth_estimateUserOperationGas"></a>

`eth_estimateUserOperationGas` is a RPC defined by the bundler; the input of this RPC is `UserOperation` and it will return a estimated data for three parameters: `callGasLimit`, `verificationGasLimit` and `preVerificationGas`.

Following the [section](../gas-and-price.md), we knew that having two main solution for estimate gas is: using `estimateGas` RPC and building a simulation for simulate the transaction operation with particular state. When you call to `eth_estimateUserOperationGas`, this RPC will run some operation:

- call `simulateValidation` to simulate the user operation execution, its result has the data that we consider estimated `verificationGasLimit`.
- call `eth_estimateGas` RPC with callData to estimate `callGasLimit`
