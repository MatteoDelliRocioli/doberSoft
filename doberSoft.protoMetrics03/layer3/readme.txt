riceve:
- comm driver

espone
	sendMessage(msg)
	{
		queue(msg)
		if comm.connect
		{
			comm.send(queue.getMsgs)
		}
	}