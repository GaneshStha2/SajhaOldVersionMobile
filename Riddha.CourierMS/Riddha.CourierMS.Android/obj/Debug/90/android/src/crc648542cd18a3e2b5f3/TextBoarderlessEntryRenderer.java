package crc648542cd18a3e2b5f3;


public class TextBoarderlessEntryRenderer
	extends crc643f46942d9dd1fff9.EntryRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Riddha.CourierMS.Droid.DependencyServices.TextBoarderlessEntryRenderer, Riddha.CourierMS.Android", TextBoarderlessEntryRenderer.class, __md_methods);
	}


	public TextBoarderlessEntryRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == TextBoarderlessEntryRenderer.class)
			mono.android.TypeManager.Activate ("Riddha.CourierMS.Droid.DependencyServices.TextBoarderlessEntryRenderer, Riddha.CourierMS.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public TextBoarderlessEntryRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == TextBoarderlessEntryRenderer.class)
			mono.android.TypeManager.Activate ("Riddha.CourierMS.Droid.DependencyServices.TextBoarderlessEntryRenderer, Riddha.CourierMS.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public TextBoarderlessEntryRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == TextBoarderlessEntryRenderer.class)
			mono.android.TypeManager.Activate ("Riddha.CourierMS.Droid.DependencyServices.TextBoarderlessEntryRenderer, Riddha.CourierMS.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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
