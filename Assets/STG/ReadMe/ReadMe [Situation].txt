起こりうる状況とその時の処理の流れ

* 攻撃目標の検出
- STGObjDetectorがObjectを検出
- STGObjSensorに渡る
- STGObjSensorが検出オブジェクトの属性を見てコールバックを投げる