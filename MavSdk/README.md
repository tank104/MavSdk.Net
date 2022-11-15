## Generate code from proto files

Most of the code is auto-generated from the proto files, versioned and kept in _Plugins/_. Whenever the templates or proto files change, they need to be generated again. This requires `protoc-gen-mavsdk` to be available in `../../proto/pb_plugins/venv/bin/protoc-gen-mavsdk`.
Ensure to get all proto using git:
```sh
git submodule init
git submodule update
```

The first time, you therefore need to install the module in a python venv. Note that you need Python 3. First go into `../../proto/pb_plugins` and create a venv:

```sh
pushd ../proto/pb_plugins
python -m venv venv
```

Then activate the venv:

```sh
source ./venv/bin/activate
```
or on Windows:
```ps
.\venv\Scripts\activate.ps1
```

protobuf version needs to be force set to 3.20.3 (since version 4 isn't compatible yet).
So edit requirements.txt and change protobuf to: 
```
protobuf==3.20.3
```

If on Windows protoc.exe needs to be downloaded and added to environment variables from here: https://github.com/protocolbuffers/protobuf/releases

You can now install `protoc-gen-mavsdk`, as follows:

```sh
pip install -r requirements.txt
pip install -e .
```



After that, running `$ which protoc-gen-mavsdk` (`PS> (gcm  protoc-gen-mavsdk).Path`) should show you the path to `protoc-gen-mavsdk`.

We can now generate the code from the proto files:

```sh
popd
dotnet build -target:genMavSdk
```
or on Windows:
```sh
popd
dotnet build -target:genMavSdkWin
```
