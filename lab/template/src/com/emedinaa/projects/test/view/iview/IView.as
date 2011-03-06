package com.emedinaa.projects.test.view.iview 
{
	import flash.events.Event;
	
	/**
	 * ...
	 * @author Eduardo José Medina Alfaro emedinaa@gmail.com
	 */
	public interface IView 
	{
		function app(e:Event=null):void;
		function open():void;
		function destroy():void;
	}
	
}