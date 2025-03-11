//-----------------------------------.
//定数.
//-----------------------------------.
// ゲームの Nexus Mods ドメイン。例: nexusmods.com/bloodstainedritualofthenight 
const GAME_ID = 'ouradventurerguild'; 
//Steam アプリケーション ID。https://steamdb.info/apps/ から取得できます。
const STEAMAPP_ID = '2026000'; 
// 拡張機能の表示名.
const VIEW_NAME = 'Our Adventurer Guild';

//-----------------------------------.
// 以下処理.
//-----------------------------------.

// Mod配置パス.
const MOD_PATH='BepInEx/plugins';

// Imports.
path = require('path'); 
const { log, util } = require('vortex-api'); 
const winapi = require('winapi-bindings'); 
const fs = require('fs-extra');
const { exec } = require('child_process');


function setChmod(dir) {
  return fs.chmod(dir, 0o777)
    .then(() => new Promise((resolve, reject) => {
        exec(`icacls "${dir}" /grant Everyone:F /T /C`, (error, stdout, stderr) => {
            if (error) {
                console.error(`権限変更エラー: ${error}`);
                reject(error);
            } else {
                console.log('アクセス権限を変更しました:', stdout);
                resolve();
            }
        });
  }));
}

//---.
// ゲーム検索.
//---.
async function findGame() { 
  var ret = "";
  ret = await util.GameStoreHelper.findByAppId(STEAMAPP_ID).then(game => game.gamePath); 
  var modFullPath = path.join(ret, MOD_PATH);
  fs.ensureDirSync(modFullPath);
  await setChmod(modFullPath);
  s_IsCreatedModPath = true;
  return ret;
}

function findModPath(){
  return MOD_PATH;
}



//---.
// Mod環境のセットアップ.
// BepInExフォルダの用意.
//---.
function prepareForModding(discovery, api) {
  const targetPath = discovery.path;
  const sourcePath = path.join(__dirname, 'BepInEx');

  return fs.readdir(sourcePath)
      .then(items => {
          const copyPromises = items.map(
            item => {
              const src = path.join(sourcePath, item);
              const dest = path.join(targetPath, item);
              return fs.copy(src, dest)
                  .then(() => setChmod(dest));
            }
          );
          return Promise.all(copyPromises);
      });
}


//-----
// Modがサポートされているかチェック.
//-----
function testSupportedContent(files, gameId) { 
  supported = (gameId === GAME_ID);
  return Promise.resolve({ 
    supported, 
    requiredFiles: [], 
  }); 
}

//-----
// Modをインストール.
//-----
function installContent(files, destinationRoot) {
  const modFolderName = path.basename(destinationRoot).replace(/\.installing$/, "");
  const rootPath = path.dirname(files[0]);

  const filtered = files.filter(file => 
    ((file.indexOf(rootPath) !== -1) 
    && (!file.endsWith(path.sep))));

  const instructions = filtered.map(file => {
    const destPath = path.join(modFolderName, file);
    return {
      type: 'copy',
      source: file,
      destination: destPath,
    };
  });
  return Promise.resolve({ instructions });
}



//-----
// Main関数.
//-----
function main(context) { 
  //先にqueryModPathだけ作っておく.
  //fs.ensureDirSync(path.join(discovery.path, 'BepInEx', 'plugins'));

	//これは、ゲーム拡張機能を検出したときにVortexが実行するメイン関数です.
	context.registerGame({ 
        id: GAME_ID, 
        name: VIEW_NAME, 
        mergeMods: true, 
        setup: prepareForModding, 
        queryPath: findGame, 
        supportedTools: [], 
        queryModPath: findModPath, 
        logo: 'gameart.png', 
        executable: () => 'Our Adventuring Guild.exe', 
        requiredFiles: [ 
          'Our Adventuring Guild.exe', 
        ], 
        environment: { 
          SteamAPPId: STEAMAPP_ID, 
        }, 
        details: { 
          steamAppId: STEAMAPP_ID
        }, 
      });
  
  context.registerInstaller(GAME_ID, 25, testSupportedContent, installContent);
	return true 
} 

module.exports = { 
    default: main, 
  };