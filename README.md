# UnityAssetstore API

> Unofficial API for controlling asset store in run-time



## Feature

- Download assets
- Retrieve assets
- Get overview of asset
- Sign in


## Usage

```csharp

// console version
static void Main(string[] args)
{
    UnityAssetstoreUser user = new UnityAssetstoreUser();

	// login to assetstore account
    user.UserLogin("your ID", "your password");
	// get assets can be downloaded
	List<int> ids = user.GetDownloadableAssets();

    UnityAssetstoreAsset asset = new UnityAssetstoreAsset();

	// download asset
    var t = aa.GetDownloadAssetTaskAsync(ids[0]);
    Task.WaitAll(t);
}

```

## Wrapper class

[AssetstoreUserWrapper](https://github.com/se0kjun/UnityAssetstoreAPI/blob/master/UnityAssetstoreAPI/wrapper/AssetstoreUserWrapper.cs)

[AssetstoreUserOverviewWrapper](https://github.com/se0kjun/UnityAssetstoreAPI/blob/master/UnityAssetstoreAPI/wrapper/AssetstoreUserOverviewWrapper.cs)

[AssetstoreContentWrapper](https://github.com/se0kjun/UnityAssetstoreAPI/blob/master/UnityAssetstoreAPI/wrapper/AssetstoreContentWrapper.cs)

[AssetstoreContentOverviewWrapper](https://github.com/se0kjun/UnityAssetstoreAPI/blob/master/UnityAssetstoreAPI/wrapper/AssetstoreContentOverviewWrapper.cs)

[AssetstoreDownloadInfoWrapper](https://github.com/se0kjun/UnityAssetstoreAPI/blob/master/UnityAssetstoreAPI/wrapper/AssetstoreDownloadInfoWrapper.cs)

## API

### UnityAssetstoreUser

#### UserLogin(string id, string password)

```
Return Type: AssetstoreUserWrapper
```

Login to Assetstore account corresponding to id and password.
 

#### GetDownloadableAssets()

```
Return Type: List<int> 
```

Get asset ids that can be downloaded.

#### GetUserOverview(string id)

```
Return Type: AssetstoreUserOverviewWrapper
```

Get user overview corresponding to user id (not email).

### UnityAssetstoreAsset


#### GetAssetOverview(int id)

```
Return Type: AssetstoreContentOverviewWrapper
```

#### GetAssetsOverview(List< int > ids)

```
Return Type: List<AssetstoreContentOverviewWrapper>
```

#### GetAssetInfo(int id)

```
Return Type: AssetstoreContentWrapper
```

#### GetAssetsInfo(List< int > ids)

```
Return Type: List<AssetstoreContentWrapper>
```

#### GetDownloadAssetTaskAsync(int id)

```
Return Type: Task<byte[]>
```

Download asset asynchronously to appdata folder. (in window case, C:\Users\Username\AppData\Roaming\Unity\Asset Store-5.x) 