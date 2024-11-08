using System;

namespace Adventurer
{
    public interface IItemCase
	{
    	public ItemData ItemData { get; }
        public Action ItemChanged { get; set; }
    }
}