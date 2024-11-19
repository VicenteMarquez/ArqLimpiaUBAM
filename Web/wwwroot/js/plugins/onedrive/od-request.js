let request = {
	//examples and test only
	getDisplayName: async () => {
		try {
			let response = await client
				.api("/me")
				.select("displayName")
				.get();
			return response.displayName;
		} catch (error) {
			console.error(error);
		}
	},
	getProfilePicture: async () => {
		try {
			let response = await client.api("/me/photo/$value").get();
			return response;
		} catch (error) {
			console.error(error);
		}
	},
	getUserDetails: async () => {
		try {
			let res = await client.api("/me").get();
			return res;
		} catch (error) {
			throw error;
		}
	},
	/////
	getMyDriveFiles: async () => {
		try {
			let res = await client.api("/me/drive/root/children").get();
			return res;
		} catch (error) {
			throw error;
		}
	},
	getFolderContent: async (idFolder) => {
		try {
			let res = await client.api("/me/drive/items/" + idFolder + "/children?expand=thumbnails").get();
			//let res = await client.api("https://graph.microsoft.com/v1.0/me/drive/root?expand=thumbnails,children(expand=thumbnails(select=large))");
			return res;
		}
		catch (error) {
			throw error;
		}
	},
	getFileInfo: async (url) => {
		try {
			let res = await client.api(url).get();
			return res;
		}
		catch (error) {
			throw error;
		}
	},
	searchFolder: async () => {
		try {
			let res = await client.api("/me/drive/root/search(q='TestAspirante')").get();
			return res;
		} catch (error) {
			throw error;
		}
	},
	createFolder: async (folderInfo) => {
		try {
			let options = {
				"name": folderInfo.NombreCompleto,
				"folder": {},
				"@microsoft.graph.conflictBehavior": "fail"
			}
			let res = await client.api("/me/drive/items/" + folderInfo.FolderId + "/children").post(options);

			let folder = {
				"OneDriveFolder_Nombre": res.name,
				"OneDriveFolder_RecursoId": res.id,
				"OneDriveFolder_ParentDriveId": res.parentReference.id,
				"OneDriveFolder_WebUrl": "https://onedrive.live.com/?id=" + res.id,
				"OneDriveFolder_Compartido": true
			};

			let opciones = {
				"type": "edit",
				"scope": "anonymous"
			}
			res = await client.api("/me/drive/items/" + res.id + "/createLink").post(opciones);
			folder.OneDriveFolder_Link = res.link.webUrl;
			res.folder = folder;

			return res;
		} catch (error) {
			throw error;
		}
	},
	removeShared: async (id) => {
		try {
			res = await client.api("/me/drive/items/" + id + "/permissions").get();

			if (res.value.length > 0 && res.value[0].id != null) {
				res = await client.api("/me/drive/items/" + id + "/permissions/" + res.value[0].id).delete();
			}

			return res;
		}
		catch (error) {
			throw error;
		}
	},
	linkSharedFolder: async (id) => {
		try {
			let opciones = {
				"type": "edit",
				"scope": "anonymous"
			}
			let res = await client.api("/me/drive/items/" + id + "/createLink").post(opciones);
			return res;
		}
		catch (error) {
			throw error;
		}
	},
	showSharedContent: async (id) => {
		try {
			let sharingUrl = "https://onedrive.live.com/?authkey=!AKlh1iIGoNeWpjs&id=17908568328D2E5B!156"
			let base64Value = btoa(sharingUrl);
			let encodedUrl = "u!" + base64Value.trimEnd('=').replace('/', '_').replace('+', '-');
			let res = await client.api("/shares/" + encodedUrl + "/driveItem?$expand=children").get();

			return res;
		}
		catch (error) {
			throw error;
		}
	}
};