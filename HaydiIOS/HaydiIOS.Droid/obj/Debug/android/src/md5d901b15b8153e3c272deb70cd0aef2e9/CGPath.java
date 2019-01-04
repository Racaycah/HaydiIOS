package md5d901b15b8153e3c272deb70cd0aef2e9;


public class CGPath
	extends android.graphics.Path
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Forms9Patch.Droid.CGPath, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CGPath.class, __md_methods);
	}


	public CGPath () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CGPath.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.CGPath, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public CGPath (android.graphics.Path p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == CGPath.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.CGPath, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Graphics.Path, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
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
