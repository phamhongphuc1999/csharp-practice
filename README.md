<h1 align="center">
My C# Practice

</h1>

| ID  | Solution                    | Note                 |
| :-: | :-------------------------- | :------------------- |
|  1  | [MyPractice](./MyPractice/) | MY practice solution |

document [here](./documents/)

### Run

- To build

  ```shell
  dotnet build
  ```

- To run

  ```shell
  dotnet run --project ./path-to-project
  ```

- To test

  ```shell
  dotnet test
  ```

### Format

- The project use `pre-commit` and python environment to install some useful package that check your code before pushing code in github. If you want to try this solution, you must create python virtual environment firstly

```shell
python -m venv venv
```

- After that, activate the environment and install pre-commit package

```shell
source ./venv/activate/bin
pip3 install -r requirements.txt
```

- Install pre-commit to github hook

```shell
pre-commit install
```

- Try to run

```shell
pre-commit run --all-file
```
