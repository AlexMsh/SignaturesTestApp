Notes:
1. This project is supposed to be a cron job, but the "cron" part is not implemented
So, at the moment, it's kind of "fire and forget"
2. This job serves the purpose of checking Postings status & downloading signed files asynchronously
	Improvement points:
	1. In a real life scenario the job wouldn't update any data in repository. Instead it would
		a. Check "New" Postings status
		b. Download signed files for "Signed" postings
		c. Gather statistics about "Expired" postings
		d. Send ids of Signed/Deleted postings via some streaming/messaging service (E.g. Kafka/RabbitMQ), so that db data could be updated accordingly

		This wasn't done in scope of test task due to 2 reasons:
		a. To keep it relatively simple
		b. To save some time on it

	2. No logging and healthmonitoring was added
	3. Job & scheduling logic (IHostedService implementation) should be separated from the actual logic
	4. Unit tests were not created

