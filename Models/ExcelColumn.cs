namespace Models
{
    /// <summary>
    /// 自定义excel头部标签
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ExcelColumn : Attribute
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public ExcelColumn(string name)
        {
            ColumnName = name;
        }
    }
}
