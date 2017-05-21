# API-FaceBook
How to use?

FBAPI api = new FBAPI(username, password);

string url_avatar_other = API.GETAvatar("me");

dynamic obj_avatar_other = JsonConvert.DeserializeObject(url_avatar_other);
