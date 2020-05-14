using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace LiCommon.Util
{
    /// <summary>
    /// 图标工具类
    /// </summary>
    public class ImageUtil
    {
        /// <summary>
        /// 图标文件夹路径
        /// </summary>
        public static string imageFolderPath = ConfigUtil.GetKey("ImageFolderPath");

        /// <summary>
        /// 16位图标缓存，组-名称-图标
        /// </summary>
        public static Dictionary<string,Dictionary<string, Image>> images16 = new  Dictionary<string,Dictionary<string, Image>>();

        /// <summary>
        /// 32位图标缓存，组-名称-图标
        /// </summary>
        public static Dictionary<string, Dictionary<string, Image>> images32 = new  Dictionary<string,Dictionary<string, Image>>();

        /// <summary>
        /// 32位图标
        /// </summary>
        public static List<ImageListModel> images;

        /// <summary>
        /// 加载所有图标
        /// </summary>
        public static void loadAllImage()
        {
            loadAllImage(imageFolderPath);
        }

        /// <summary>
        /// 转换成对象列表
        /// </summary>
        public static void convertImageList()
        {

            images = new List<ImageListModel>();
            foreach (KeyValuePair<string, Dictionary<string, Image>> kvps in ImageUtil.images32)
            {
                foreach (KeyValuePair<string, Image> kvp in kvps.Value)
                {
                    ImageListModel imageListModel = new ImageListModel() { name = kvps.Key + "|" + kvp.Key, image = kvp.Value };
                    images.Add(imageListModel);
                }
            }

        }

        /// <summary>
        /// 加载所有图标
        /// </summary>
        /// <param name="imageFolderPath"></param>
        public static void loadAllImage(string imageFolderPath){
            DirectoryInfo d = new DirectoryInfo(imageFolderPath);
            //实例化DirectoryInfo
            FileSystemInfo[] f = d.GetFileSystemInfos();
            //获取指定文件夹中子文件夹和文件
            
            foreach (FileSystemInfo fs in f)
            {
                if (fs is DirectoryInfo)
                {
                    if (!images16.ContainsKey(fs.Name))
                    {
                        images16.Add(fs.Name, new Dictionary<string, Image>());

                    }
                    if (!images32.ContainsKey(fs.Name))
                    {
                        images32.Add(fs.Name, new Dictionary<string, Image>());
                    }
                    Dictionary<string, Image> image16Dict = images16[fs.Name];
                    Dictionary<string, Image> image32Dict = images32[fs.Name];

                    //判断遍历出的是文件夹
                    DirectoryInfo di = new DirectoryInfo(fs.FullName);
                    FileSystemInfo[] fi = di.GetFileSystemInfos();
                    foreach (FileSystemInfo fsi in fi)
                    {
                        if(fsi.Name.LastIndexOf("_16x16") > 0){
                            if(!image16Dict.ContainsKey(fsi.Name))
                                image16Dict.Add(fsi.Name, Bitmap.FromFile(fsi.FullName));
                        }

                        if (fsi.Name.LastIndexOf("_32x32") > 0)
                        {
                            if (!image32Dict.ContainsKey(fsi.Name))
                                image32Dict.Add(fsi.Name, Bitmap.FromFile(fsi.FullName));
                        }
                    }

                }
            }

            convertImageList();

        }

        public static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="key">图标名</param>
        /// <param name="size">图标大小</param>
        /// <returns></returns>
        public static Image getBitmap(string groupName,string key, string size)
        {
            if (!images16.ContainsKey(groupName))
            {
                Dictionary<string, Image> imageNew = new Dictionary<string,Image>();
                images16.Add(groupName, imageNew);
            }
            
            Dictionary<string, Image> imageDict =images16[groupName];
            if(!imageDict.ContainsKey(key)){
                string fileName = string.Format("{0}{1}{2}{3}{4}", imageFolderPath, "\\Bitmap", string.Format("\\{0}\\", groupName), key, string.Format("{0}.png",size));
                if(File.Exists(fileName))
                {
                    imageDict.Add(key, Bitmap.FromFile(fileName));
                }
                else
                {
                    return null;
                }
            }

            return imageDict[key];

        }

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="key">图标名</param>
        /// <param name="size">图标大小</param>
        /// <returns></returns>
        public static Image getBitmap(string groupName, string key)
        {
            if (!images16.ContainsKey(groupName))
            {
                Dictionary<string, Image> imageNew = new Dictionary<string, Image>();
                images16.Add(groupName, imageNew);
            }

            Dictionary<string, Image> imageDict = images16[groupName];
            if (!imageDict.ContainsKey(key))
            {
                string fileName = string.Format("{0}{1}{2}{3}", imageFolderPath, "\\Bitmap", string.Format("\\{0}\\", groupName), key);
                if (File.Exists(fileName))
                {
                    imageDict.Add(key, Bitmap.FromFile(fileName));
                }
                else
                {
                    return null;
                }

            }

            return imageDict[key];

        }
        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Image getBitmap32(string groupName,string key)
        {
            return getBitmap(groupName, key,"_32x32");
        }

        /// <summary>
        /// 获取图标
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Image getBitmap16(string groupName,string key)
        {
            return getBitmap(groupName, key,"_16x16");
        }


        /// <summary>
        /// 图标列表模型,内部类
        /// </summary>
        public class ImageListModel
        {
            /// <summary>
            /// 搜索列
            /// </summary>
            public static List<string> searchColumns = new List<string>();

            /// <summary>
            /// 显示列
            /// </summary>
            public static List<string> displayColumns = new List<string>();

            /// <summary>
            /// 列名映射
            /// </summary>
            public static Dictionary<string, string> dictModelDesc = new Dictionary<string, string>();

            /// <summary>
            /// 图标名称
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 图标
            /// </summary>
            public Image image { get; set; }

            /// <summary>
            /// 获取搜索列
            /// </summary>
            public static List<string> getSearchColumns()
            {
                if (!searchColumns.Contains("name"))
                {
                    searchColumns.Add("name");
                }
                return searchColumns;
            }

            /// <summary>
            /// 获取显示列
            /// </summary>
            public static List<string> getDisplayColumns()
            {
                if (!displayColumns.Contains("image"))
                {
                    displayColumns.Add("image");
                }
                if (!displayColumns.Contains("name"))
                {
                    displayColumns.Add("name");
                }
                return displayColumns;
            }

            /// <summary>
            /// 获取列名映射
            /// </summary>
            public static Dictionary<string, string> getDictModelDesc()
            {
                if (!dictModelDesc.ContainsKey("image"))
                {
                    dictModelDesc.Add("image", "图像");
                }
                if (!dictModelDesc.ContainsKey("name"))
                {
                    dictModelDesc.Add("name", "名称");
                }
                return dictModelDesc;
            }

        }
    }


}
