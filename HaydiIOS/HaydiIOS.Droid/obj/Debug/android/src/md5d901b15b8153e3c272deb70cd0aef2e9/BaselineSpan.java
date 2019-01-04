package md5d901b15b8153e3c272deb70cd0aef2e9;


public class BaselineSpan
	extends android.text.style.MetricAffectingSpan
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_updateDrawState:(Landroid/text/TextPaint;)V:GetUpdateDrawState_Landroid_text_TextPaint_Handler\n" +
			"n_updateMeasureState:(Landroid/text/TextPaint;)V:GetUpdateMeasureState_Landroid_text_TextPaint_Handler\n" +
			"";
		mono.android.Runtime.register ("Forms9Patch.Droid.BaselineSpan, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaselineSpan.class, __md_methods);
	}


	public BaselineSpan () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaselineSpan.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.BaselineSpan, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public BaselineSpan (float p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaselineSpan.class)
			mono.android.TypeManager.Activate ("Forms9Patch.Droid.BaselineSpan, Forms9Patch.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.Single, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public void updateDrawState (android.text.TextPaint p0)
	{
		n_updateDrawState (p0);
	}

	private native void n_updateDrawState (android.text.TextPaint p0);


	public void updateMeasureState (android.text.TextPaint p0)
	{
		n_updateMeasureState (p0);
	}

	private native void n_updateMeasureState (android.text.TextPaint p0);

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
