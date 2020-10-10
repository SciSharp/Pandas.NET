# Pandas.NET

[![Join the chat at https://gitter.im/publiclab/publiclab](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/sci-sharp/community) [![NuGet](https://img.shields.io/nuget/dt/Pandas.NET.svg)](https://www.nuget.org/packages/Pandas.NET)

## Implemented APIs

### 1. Pandas

* DataFrame
  * `pd.DataFrame(NDArray  data, IList<string> index,  IList<string> columns, Type dtype)`
  * `pd.DataFrame<TIndex>(NDArray  data, IList<TIndex> index,  IList<string> columns, Type dtype)`
  * `pd.DataFrame(IDictionary<string, NDArray> data, IList<string> index)`
  * `pd.DataFrame<TIndex>(IDictionary<string, NDArray> data, IList<TIndex> index)`
* Series
  * `pd.Series(NDArray data)`
  * `pd.Series(Array data)`
  * `pd.Series<T>(T data)`

### 2. Series

* `s.iloc[0]`: Select data by index

* `s.loc["index_label"]`： Select data by index label

### 3. DataFrame

#### Structure

* df.Index
* df.Columns
* df.Values
* df.Shape
* df.NDIM
* df.Size

#### Method

* `df[0]`： Select data by column index （returns Series）
* `df[params int[] columnIndexs] `: Select data by column index （returns DataFrame）
* `df["column_label"]`： Select data by column label （returns Series）; accessible `set` accessor increase column （if the column label does not exist）
* `df[params string[] columnLabels]`： Select data by column label （returns DataFrame）
* `df.Column(string columnLabel, NDArray value)`: Set the column and its value; create when the column does not exist
* `df.Column(int columnIndex, NDArray value)`: Set the column and the value of the column; when the column does not exist, an exception is reported
* `df[Slice s]`: Row slice selection data

* `df.loc["index_label"]`： Select data by row index label
* `df.loc["index_label", "column_label"]`： Select data by row and column labels
* `df.iloc[0]`： Index by row （row number） select data
