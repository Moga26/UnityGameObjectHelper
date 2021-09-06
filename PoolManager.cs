using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PoolManager : MonoBehaviour
{
	public static PoolManager instance;
	Dictionary<string, List_Pool_slot> slots = new Dictionary< string, List_Pool_slot >();
	char unique_char = (char) 0;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		StartCoroutine( CheckSlots() );
	}

	List_Pool_slot AddOneSlot( AudioClip clip, AudioSource source )
	{
		List_Pool_slot psls = new List_Pool_slot( clip, source );
		slots.Add( psls.Get_ID(), psls );

		return psls;
    }

    IEnumerator CheckSlots()
	{
		while ( true )
		{
			IEnumerable< KeyValuePair< string, List_Pool_slot > > to_remove_slots = 
					slots.Where( x=> x.Value.Add_one_inactivity_point() );


			foreach ( KeyValuePair<string, List_Pool_slot> kvp in to_remove_slots.ToList() )
			{
				slots.Remove( kvp.Key );
				kvp.Value.Destroy_fun();
			}
			yield return new WaitForSeconds( 1f );
		}
	}

	public Pool_slot GetOneSlot( AudioClip clip, AudioSource source )
	{
		List_Pool_slot psls;
        
        if ( slots.ContainsKey( clip.name ) )
		{
			psls = slots[ clip.name ];
		}
		else
		{
			psls = AddOneSlot( clip, source );
     
        }

		return psls.GetOneSlot();
    }
}