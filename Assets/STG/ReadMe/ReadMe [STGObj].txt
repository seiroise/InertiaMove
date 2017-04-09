STGObjの構造に関するReadMe

STGObjと呼んでいる階層構造を持ったオブジェクトを構成している部品に関して、
いろいろと忘れないようにメモをとっていく。
一応かるめの仕様書的な意味合いも兼ねている。


// ComSystem //
STGObjを構成している基本的な仕組み

* STGCom : MonoBehaviour
- 概要
	STGComManagerへの参照を持つComSystemで一番小さなクラス
- 仮想メソッド
	- STGInit(STGComManager)
		STGComの初期化時に一度だけ呼ばれる
	- STGAwake()
		STGComの初期化後に一度だけ呼ばれる

* STGAbstractComManager<Com> : STGCom where Com : STGCom
- 概要
	型パラメータ2指定したSTGComを継承するクラスを複数持ち管理するための抽象クラス
	基本的にUnityのエディタ上で子STGComの管理を行う

* STGComManager : STGAbstractComManager<STGCom>
- 概要
	STGAbstractComManagerの型パラメータにSTGComクラスを指定したクラス
	STGAbstractComManagerを実際に使用するための具象クラス

// STGObj //
ComSystemを使用した階層構造を持つシューティングゲーム用のオブジェクト

* STGObj : STGComManager
- 概要
	STGObjを構成するためのルートクラス
	最低限必要なSTGComとして
	STGObjMarker			: STGObjの位置をマップに表示するマーカーを制御するためのSTGCom
	STGObjSensor			: STGObjの
	STGObjTargetingResolver	: STGObjの自動ターゲッティングを行うためのSTGCom
	STGObjArmor				: STGObjの装甲値(0になると破壊)を管理するためのSTGCom
	STGObjAttitudeController: STGObjの姿勢制御を行う
	の5つのSTGComへのアクセサを持つ

// STGObjCom //
ComSystemを利用したSTGObjの子クラス(クラス名ではない)
StgObjComの一覧
- Addon
	STGObjの追加機能
- Armor
	STGObjの装甲
- Attitude
	STGObjの姿勢制御
- Equipment
	STGObjの装備
- Marker
	STGObjの位置表示
- Sensor
	他STGObjの接近などを感知
- Targeting
	STGObjの自動ターゲッティング
- Thruster
	STGObjの推進装備
- Weapon
	STGObjの武器装備

// Addon //
* STGObjAddon : STGObjEquipment
* STGObjAddonController : STGObjEquipmentController<STGObjAddonSlot, STGObjAddon>
* STGObjAddonSlot : STGObjEquipmentSlot<STGObjAddon>

// Armor //
* STGObjArmor : STGCom
- 概要
	ひとつのAttackableObject2Dへの参照を持つSTGObjの装甲を管理するクラス
	実際に攻撃を受けるのはAttackableObject2D、
	位置付け的にはAttackableObject2DとSTGObjのアダプタ
	(AttackableObject2Dは汎用クラスであるため、そのまま組み込まないように)

// Attitude //
* STGObjAttitudeController : STGCom
- 概要
	STGObjにアタッチされているSTGObjThrusterControllerを介しての推進や回転を制御するためのクラス
	外部から操作する場合はSTGObjThrusterControllerを介して行うのではなくこちらを使う

// Equipment //
* STGObjEquipment : STGCom
- 概要
	STGObj用の装備の基底クラス
	Addon. Thruster, Weapo などの装備クラスはこのクラスを継承している
- 仮想メソッド
	- StandUpEquipment()
		装備を起動状態へ変更する
	- StandDownEquipment()
		装備を待機状態へ変更する

*STGObjEquipmentController : <Slot, Equipment> : STGAbstractComManager<Slot>
	where Slot : STGObjEquipmentSlot<Equipment>
	where Equipment : STGObjEquipment
- 概要
	STGObj用の装備を管理するクラス
	基本的に一つの種類に関して複数のスロットで管理を行う
- 仮想メソッド
	- SetEquipment(Equipment, bool)
		指定した装備を空いているスロットに設定する
		prefabからの生成を同時に行う場合は第二引数をfalseにする
		スロットが空いていなかった場合はnullを返し
		スロットが空いていた場合は設定し装備を返す
	- RemoveEquipment(int index)
		指定したスロットの装備を解除する
		戻り値として解除した装備を返す

* STGObjEquipmentSlot<Equipment> : STGCom where Equipment : STGObjEquipment
-概要
	STGObj用の装備スロット
	基本的にこれと1対1で対応する装備をセットする

// Marker //
* STGObjMarker
- 概要
	STGObjの現在位置とマーカーの色による状態を表示する

// Sensor //