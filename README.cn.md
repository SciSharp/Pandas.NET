# Pandas.NET

[![Join the chat at https://gitter.im/publiclab/publiclab](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/sci-sharp/community)
[![NuGet](https://img.shields.io/nuget/dt/Pandas.NET.svg)](https://www.nuget.org/packages/Pandas.NET)
[![Build Status](https://dev.azure.com/scisharp/Pandas.NET/_apis/build/status/Windows%20CI?branchName=master&label=Windows)](https://dev.azure.com/scisharp/Pandas.NET/_build/latest?definitionId=2&branchName=master)
[![Build Status](https://dev.azure.com/scisharp/Pandas.NET/_apis/build/status/Ubuntu%20CI?branchName=master&label=Ubuntu)](https://dev.azure.com/scisharp/Pandas.NET/_build/latest?definitionId=3&branchName=master)
[![Build Status](https://dev.azure.com/scisharp/Pandas.NET/_apis/build/status/macOS%20CI?branchName=master&label=MacOS)](https://dev.azure.com/scisharp/Pandas.NET/_build/latest?definitionId=1&branchName=master)

## Implemented APIs

### 1. Pandas

- DataFrame
  - `pd.DataFrame(NDArray  data, IList<string> index,  IList<string> columns, Type dtype)`
  - `pd.DataFrame<TIndex>(NDArray  data, IList<TIndex> index,  IList<string> columns, Type dtype)`
  - `pd.DataFrame(IDictionary<string, NDArray> data, IList<string> index)`
  - `pd.DataFrame<TIndex>(IDictionary<string, NDArray> data, IList<TIndex> index)`
- Series
  - `pd.Series(NDArray data)`
  - `pd.Series(Array data)`
  - `pd.Series<T>(T data)`

### 2. Series

- `s.iloc[0]`:按索引选取数据

- `s.loc["index_label"]`：按索引标签选取数据

### 3. DataFrame

#### 结构

- df.Index
- df.Columns
- df.Values
- df.Shape
- df.NDIM
- df.Size

#### 方法

- `df[0]`：按列索引选取数据（返回 Series）
- `df[params int[] columnIndexs] `:按列索引选取数据（返回 DataFrame）
- `df["column_label"]`：按列标签选取数据（返回 Series）;可通过 `set` 访问器增加列（如果列标签不存在）
- `df[params string[] columnLabels]`：按列标签选取数据（返回 DataFrame）
- `df.Column(string columnLabel, NDArray value)`:设置列以及列的值；当列不存在时创建
- `df.Column(int columnIndex, NDArray value)`:设置列以及列的值；当列不存在时报异常
- `df[Slice s]`:行切片选取数据

- `df.loc["index_label"]`：按行索引标签选取数据
- `df.loc["index_label", "column_label"]`：按行和列标签选取数据
- `df.iloc[0]`：按行索引（row number）选取数据

## 函数库

- [Math.NET Numerics](https://numerics.mathdotnet.com/)
  - [MIT License](https://github.com/mathnet/mathnet-numerics/blob/master/LICENSE.md)
