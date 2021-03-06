package md553d2d7a440f8bd2ac6b092e91fad963d;


public class NativeGestureDetector
	extends android.view.GestureDetector
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnTouchListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTouchEvent:(Landroid/view/MotionEvent;)Z:GetOnTouchEvent_Landroid_view_MotionEvent_Handler\n" +
			"n_onTouch:(Landroid/view/View;Landroid/view/MotionEvent;)Z:GetOnTouch_Landroid_view_View_Landroid_view_MotionEvent_Handler:Android.Views.View/IOnTouchListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("FormsGestures.Droid.NativeGestureDetector, FormsGestures.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NativeGestureDetector.class, __md_methods);
	}


	public NativeGestureDetector (android.content.Context p0, android.view.GestureDetector.OnGestureListener p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == NativeGestureDetector.class)
			mono.android.TypeManager.Activate ("FormsGestures.Droid.NativeGestureDetector, FormsGestures.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Views.GestureDetector+IOnGestureListener, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public NativeGestureDetector (android.view.GestureDetector.OnGestureListener p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == NativeGestureDetector.class)
			mono.android.TypeManager.Activate ("FormsGestures.Droid.NativeGestureDetector, FormsGestures.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Views.GestureDetector+IOnGestureListener, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public boolean onTouchEvent (android.view.MotionEvent p0)
	{
		return n_onTouchEvent (p0);
	}

	private native boolean n_onTouchEvent (android.view.MotionEvent p0);


	public boolean onTouch (android.view.View p0, android.view.MotionEvent p1)
	{
		return n_onTouch (p0, p1);
	}

	private native boolean n_onTouch (android.view.View p0, android.view.MotionEvent p1);

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
