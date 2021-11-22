package crc648542cd18a3e2b5f3;


public class DroidCustomShellPageItemRenderer
	extends crc643f46942d9dd1fff9.ShellItemRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Riddha.CourierMS.Droid.DependencyServices.DroidCustomShellPageItemRenderer, Riddha.CourierMS.Android", DroidCustomShellPageItemRenderer.class, __md_methods);
	}


	public DroidCustomShellPageItemRenderer ()
	{
		super ();
		if (getClass () == DroidCustomShellPageItemRenderer.class)
			mono.android.TypeManager.Activate ("Riddha.CourierMS.Droid.DependencyServices.DroidCustomShellPageItemRenderer, Riddha.CourierMS.Android", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
