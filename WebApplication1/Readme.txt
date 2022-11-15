Notes:
Improvement points:
	1. Unit tests were not created
	2. Ideally we would probably have Application layer (see FileController)
	3. No logging & healthmonitoring
	4. Paging wasn't added for the SignatureData list endpoint (for the real life scenarios paging would be a must)
	5. Sensitive configuration should be hidden (different approaches could be used)
		a. Encoding config file
		b. Using any KeyVault
		c. ?
		(For now some config setting values were simply ommited in config files)

Concept:
1. We separate contracts from the actual implementation so that:
	a. contracts could be shared
	b. We kind of "scope/limit" dependencies this way

2. Each implementation Library registers it's own dependencies (DI)

3. Signant API usage is under the proxy/facade (see Signatures.Client.Proxy.Contracts & Signatures.Signant.Client.Proxy)
	a. Note this is done just to highlight the concept of keeping own code (especially Domain) as far as possible from the 3-rd party API clients
	IN THEORY that allows us to adjust to minor API changes without any changes in Domain, as well as adding other signature provides

4. Helpers do not follow the contracts/implementation pattern (it could and ideally should, but in scope of this test task, leaving it as is should be fine)

5. DomainException is created to highlight the concept of error handling filters/middleware, based on the base class we could "recognize" the type of the exception and handle them differently
	a. Note that the actual implementation is super trivial
	b. Note that in reality the implementation would also depend on env. (e.g. dev/prod would expose different set of values & logs)



Side note:
Did You consider creating a Nuget package with an "SDK"?
I think it could be pretty much the same client, generated from WSDL.

Benefits for the clients:
1. Easier to manage versions/contract changes (e.g. just a Nuget package update)
2. No need to bother with wsdl(s), the client is already there
3. All the code samples could be based on it? (E.g. it was a bit confusing for me where those clients, mentioned in "examples" in Your API doc are comming from)
