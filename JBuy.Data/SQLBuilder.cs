using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JBuy.Data
{
	public enum SQLCommandType
	{
		SELECT,
		INSERT,
		UPDATE,
        DELETE
	}

	public interface ISQLCommandSaver
	{
		void SaveTo(SQLCommandBase scb);
	}

	public class SQLBuilder
	{
		public static SQLInsertCommand INSERT
		{
			get
			{
				return new SQLInsertCommand();
			}
		}

		public static SQLInsertOrUpdateCommand INSERTORUPDATE
		{
			get
			{
				return new SQLInsertOrUpdateCommand();
			}
		}

		public static SQLUpdateCommand UPDATE
		{
			get
			{
				return new SQLUpdateCommand();
			}
		}

	    public static SQLDeleteCommand DELETE
	    {
	        get
	        {
	            return new SQLDeleteCommand();
	        }
	    }

	    public static SQLSelectCommand SELECT
	    {
	        get
	        {
	            return new SQLSelectCommand();
	        }
	    }
	}

    /// <summary>
    /// 定义SQL表里的一个元素
    /// </summary>
	public class SQLField : Tuple<string, string, bool>
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="iskey">是否主键</param>
		public SQLField(string name, string value, bool iskey) : base(name, value, iskey) { }

		public string Field { get { return Item1; } }
		public string Value { get { return Item2; } }
		public bool IsKey { get { return Item3; } }
	}

	public class SQLFields : List<SQLField>
	{
		public IEnumerable<string> Fileds
		{ get { return this.Select(field => field.Item1); } }

		public IEnumerable<string> Values
		{ get { return this.Select(field => field.Item2); } }
	}

	public abstract class SQLCommandBase
	{
		protected SQLCommandType _CommandType;
		public SQLCommandType CommandType
		{
			get { return _CommandType; }
		}

		protected string _TableName;
		public string TableName
		{
			get { return _TableName; }
		}

		protected string _WhereString = string.Empty;
		public string WhereString
		{
			get { return _WhereString; }
		}

		protected readonly SQLFields _Fields = new SQLFields();
		public SQLFields Fields
		{
			get { return _Fields; }
		}

		protected SQLCommandBase()
		{
		}

		public virtual SQLCommandBase VALUE(string field, string value, bool iskey = false)
		{
			_Fields.Add(new SQLField(string.Format("`{0}`", field), string.Format("'{0}'", value), iskey));
			return this;
		}

		public virtual SQLCommandBase TABLE(string table_name)
		{
			_TableName = table_name;
			return this;
		}

		public virtual SQLCommandBase WHERE(string where_string)
		{
			_WhereString = where_string;
			return this;
		}

		public abstract string GetSQL();

		public override string ToString()
		{
			return GetSQL();
		}

		public virtual SQLCommandBase Save(ISQLCommandSaver saver)
		{
			saver.SaveTo(this);
			return this;
		}
	}

	public class SQLInsertCommand : SQLCommandBase
	{
		public SQLInsertCommand()
		{
			_CommandType = SQLCommandType.INSERT;
		}

		private bool _Ignore = false;

		public override SQLCommandBase WHERE(string where_string)
		{
			throw new Exception("INSERT操作不能设置WHERE条件");
		}

		public SQLCommandBase IGNORE
		{
			get
			{
				_Ignore = true;
				return this;
			}
		}

		public override string GetSQL()
		{
			return string.Format("INSERT{3} INTO `{0}`({1}) VALUES({2})",
				_TableName, string.Join(",", _Fields.Fileds), string.Join(",", _Fields.Values), _Ignore ? " IGNORE" : string.Empty);
		}
	}

	public class SQLInsertOrUpdateCommand : SQLCommandBase
	{
		public SQLInsertOrUpdateCommand()
		{
			_CommandType = SQLCommandType.INSERT;
		}

		public override SQLCommandBase WHERE(string where_string)
		{
			throw new Exception("INSERT操作不能设置WHERE条件");
		}

		public override string GetSQL()
		{
			return string.Format("INSERT INTO `{0}`({1}) VALUES({2}) ON DUPLICATE KEY UPDATE {3}",
				_TableName, string.Join(",", _Fields.Fileds), string.Join(",", _Fields.Values),
				string.Join(",", _Fields.Where(field => !field.IsKey).Select(field => field.Field + "=" + field.Value)));
		}
	}

	public class SQLUpdateCommand : SQLCommandBase
	{
		public SQLUpdateCommand()
		{
			_CommandType = SQLCommandType.UPDATE;
		}

		public override string GetSQL()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("UPDATE `" + _TableName + "` SET ");
			List<string> param = new List<string>();
			foreach (SQLField field in _Fields)
			{
				param.Add(field.Field + "=" + field.Value);
			}
			sb.Append(string.Join(",", param));
			if (!string.IsNullOrWhiteSpace(_WhereString))
				sb.Append(" WHERE " + _WhereString);
			return sb.ToString();
		}
	}

    public class SQLDeleteCommand : SQLCommandBase
    {
        public SQLDeleteCommand()
        {
            _CommandType = SQLCommandType.DELETE;
        }

        public override string GetSQL()
        {
            return string.Format("DELETE FROM `{0}` WHERE {1}",
                _TableName,_WhereString);
        }

        public override SQLCommandBase VALUE(string field, string value, bool iskey = false)
        {
            throw new Exception("DELETE操作不能设置VALUE值");
        }
    }

    public class SQLSelectCommand : SQLCommandBase
    {
        public SQLSelectCommand IsOnly()
        {
            _isOnly = true;
            return this;
        }

        private bool _isOnly = false;

        public SQLSelectCommand()
        {
            _CommandType = SQLCommandType.SELECT;
        }

        public override string GetSQL()
        {
            return string.Format("SELECT * FROM `{0}` WHERE {1} {2}",
                _TableName, _WhereString, _isOnly ? "LIMIT 0,1" : String.Empty);
        }

        public override SQLCommandBase VALUE(string field, string value, bool iskey = false)
        {
            throw new Exception("SELECT操作不能设置VALUE值");
        }
    }
}
