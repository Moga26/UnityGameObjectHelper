public class ListPoolSlot
{
	List<Pool_slot> psls = new List<Pool_slot>();
	AudioClip clip;
	AudioSource source;

	public ListPoolSlot( AudioClip aclip, AudioSource asource )
	{
		clip 	= aclip;
		source 	= asource;
	}

	public bool AddOneInactivityPoint()
	{
		bool all_inactive 			= true;
		List<Pool_slot> to_remove 	= new List<Pool_slot>();

		foreach( Pool_slot psl in psls )
		{
			if ( psl.AddOneInactivityPoint() )
			{
				psl.DestroyFun();
				to_remove.Add( psl );
			}
			else
			{
				all_inactive = false;
			}
		}

		to_remove.ForEach( x => psls.Remove( x ) );
		return all_inactive;
	}

	public Pool_slot GetOneSlot( )
	{
		foreach( Pool_slot psl in psls )
		{	
			if ( !psl.Is_busy() )
			{
				return psl;
			}
		}
		return AddOneSlot(); 
	}

	Pool_slot AddOneSlot()
	{
		Pool_slot psl = new Pool_slot( clip.name, source, 2 );
		psls.Add( psl );

		return psl;
	}

	public string GetID()
	{
		return clip.name;
	}
	
	public void DestroyFun()
	{
		//Destroy ( this );
	}
}