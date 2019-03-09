# Pandas.NET



## Implemented APIs

### 1. Pandas

* DataFrame
  * `pd.DataFrame(NDArray  data, IList<string> index,  IList<string> columns, Type dtype)`
  * `pd.DataFrame<TIndex>(NDArray  data, IList<TIndex> index,  IList<string> columns, Type dtype)`
  * `pd.DataFrame(IDictionary<string, NDArray> data, IList<string> index)`
  * `pd.DataFrame<TIndex>(IDictionary<string, NDArray> data, IList<TIndex> index)`
* Series
  * `pd.Series(NDArray data, IDataIndex index=null)`
  * `pd.Series<T>(T data, IDataIndex index=null)`

### 2.  Series

* `s.iloc[0]`:按索引选取数据

* `s.loc["index_label"]`：按索引标签选取数据

### 3. DataFrame

#### 结构

* df.Index
* df.Columns
* df.Values
* df.Shape
* df.NDIM
* df.Size

#### 方法

* `df[0]`：按列索引选取数据（返回Series）
* `df[params int[] columnIndexs] `:按列索引选取数据（返回DataFrame）
* `df["column_label"]`：按列标签选取数据（返回Series）;可通过 `set` 访问器增加列（如果列标签不存在）
* `df[params string[] columnLabels]`：按列标签选取数据（返回DataFrame）
* `df.Column(string columnLabel, NDArray value)`:设置列以及列的值；当列不存在时创建
* `df.Column(int columnIndex, NDArray value)`:设置列以及列的值；当列不存在时报异常
* df[Slice s]:行切片选取数据

* `df.loc["index_label"]`：按行索引标签选取数据
* `df.loc["index_label", "column_label"]`：按行和列标签选取数据
* `df.iloc[0]`：按行索引（row number）选取数据

