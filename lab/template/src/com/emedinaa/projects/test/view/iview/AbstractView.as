package com.emedinaa.projects.test.view.iview 
{
	import flash.display.Sprite;
	import flash.events.Event;
	
	/**
	 * ...
	 * @author Eduardo José Medina Alfaro emedinaa@gmail.com
	 */
	public class AbstractView extends Sprite implements IView 
	{
		
		public function AbstractView() 
		{
			super();
			if (stage) { app(); } else
			{ this.addEventListener(Event.ADDED_TO_STAGE, app)}
			
		}
		
		/* INTERFACE com.emedinaa.projects.test.view.iview.IView */
		
		public function app(e:Event=null):void 
		{
			this.removeEventListener(Event.ADDED_TO_STAGE, app);
			open();
		}
		
		public function open():void 
		{
			trace("open view ");
		}
		
		public function destroy():void 
		{
			
		}
		
	}

}