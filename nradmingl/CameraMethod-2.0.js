/*
*  @Author: 	沈爱民
*  @address:  wenzhou
*/

//使用前请使用正确Flash路径.
var cameraSwfPath = 'Camera3.swf'

document.write(
    "<div id='CameraPanel'  style='width:100%;height:100%'>" +
        "<object id='WZCameraCode' name='WZCamera' style='border:solid 0px red' classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,0,0' height='100%' width='100%'>" +
            "<param name='src' value='" + cameraSwfPath + "' />" +
            "<param name='allowScriptAccess' value='always' /> " +
        "</object>" +
    "</div>");
var CameraMethod = {
    swfName: 'WZCamera',
    Cameraer: null,
    getCamera: function(swfName) {
        if (navigator.appName.indexOf('Microsoft') != -1) {
            return document.forms[0][swfName];
        } else {
            return document[swfName];
        }
    },
    setWebServiceUrl: function(filename) {
        this.Cameraer.setWebServiceUrl(filename);
    },
    uploadedFunction: function(filePath) {
        this.Cameraer.InitUploadedFunction(filePath);
    },
    onAvtived: function() {
        this.Cameraer.onAvtived();
    },
    takePhoto: function() {
        this.Cameraer.takePhoto();
    },
    savePhoto: function() {
        this.Cameraer.savePhoto();
    },
    takeAgain: function() {
        this.Cameraer.takeAgain();
    },
    InitCamera: function() {
        this.Cameraer.InitCamera();
    },
    onReady: function() {
        try {
            this.Cameraer = this.getCamera(this.swfName);
            initCamera();
        } catch (e) {
            setTimeout('CameraMethod.onReady()', 50);
        }
    }
};
CameraMethod.onReady();