using System.Runtime.Serialization;

namespace ContractInterfaces
{
	[DataContract]
	public class IntervalValue<T>
	{
		[DataMember]
		public T Min { get; set; }

		[DataMember]
		public T Max { get; set; }
	}
}