let client;

const Secrets = {
	clientId: "57e20617-0e4f-4556-b004-6ae3b87ea035",
};

const init = async () => {
	const scopes = ["user.read", "Files.Read", "Files.Read.All", "Files.ReadWrite", "Files.ReadWrite.All"];
	const msalConfig = {
		auth: {
			clientId: Secrets.clientId,
			redirectUri: "http://localhost:54385/Global/Documento/OneDriveConnector",
		},
	};

	var msalApplication = new Msal.UserAgentApplication(msalConfig);
	const msalOptions = new MicrosoftGraph.MSALAuthenticationProviderOptions(scopes);
	const msalProvider = new MicrosoftGraph.ImplicitMSALAuthenticationProvider(msalApplication, msalOptions);
	client = MicrosoftGraph.Client.initWithMiddleware({
		debugLogging: true,
		authProvider: msalProvider,
	});

};