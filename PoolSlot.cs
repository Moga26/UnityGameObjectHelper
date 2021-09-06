public class PoolSlot
{
	public string name;
	public Component comp;
	int count_inactive;
	int max_inactive;
		
	public PoolSlot ( string aname, Component acomp, int amax_inactive )
	{
		name 			= aname;
		comp 			= Instantiate( acomp );
		max_inactive 	= amax_inactive;
		count_inactive 	= 0;
	}
		
	public bool AddOneInactivityPoint()
	{
		if ( !IsBusy() && ++count_inactive >= max_inactive )
		{
			return true;
		}
		return false;
	}
		
	public bool IsBusy()
	{
		bool busy = ( comp as AudioSource ).isPlaying;
		if ( busy )
		{
			count_inactive = 0;
		}

		return busy;
	}
		
	public void DestroyFun()
	{
		Destroy ( comp.gameObject );
	}

	public void Use()
	{
		( comp as AudioSource ).Play() ;
	}
}