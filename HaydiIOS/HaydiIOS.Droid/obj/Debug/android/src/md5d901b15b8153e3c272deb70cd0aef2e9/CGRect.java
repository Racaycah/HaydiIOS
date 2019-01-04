package md5d901b15b8153e3c272deb70cd0aef2e9;


public class CGRect
	extends android.graphics.RectF
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Forms9Patch.Droid.CGRect, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", CGRect.class, __md_methods);
	}


	public CGRect () throws java.lang.Throwable
	{
		super ();
		if (getClass () == CGRect.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.CGRect, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public CGRect (android.graphics.Rect p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == CGRect.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.CGRect, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Graphics.Rect, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public CGRect (android.graphics.RectF p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == CGRect.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.CGRect, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Graphics.RectF, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public CGRect (float p0, float p1, float p2, float p3) throws java.lang.Throwable
	{
		super (p0, p1, p2, p3);
		if (getClass () == CGRect.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.CGRect, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.Single, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Single, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Single, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Single, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}

	public CGRect (md5d901b15b8153e3c272deb70cd0aef2e9.CGRect p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == CGRect.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.CGRect, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Forms9Patch.Droid.CGRect, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
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
