using System;
using System.Collections.Generic;
using System.Text;
using u_doit.I_DoIt.Objects;

namespace u_doit.I_DoIt
{
    class ObjectTypeCategoriesResponse
    {
        public List<CategoryInfo> catg;
        public List<CategoryInfo> cats;

        public bool TestForConstant(string constName)
        {
            if (catg != null)
            {
                foreach (CategoryInfo ci in catg)
                {
                    if (ci.constant.Equals(constName))
                        return true;
                }
            }

            if (cats != null)
            {
                foreach (CategoryInfo ci in cats)
                {
                    if (ci.constant.Equals(constName))
                        return true;
                }
            }

            return false;
        }

        public int ConstToId(string constName)
        {
            if (catg != null)
            {
                foreach (CategoryInfo ci in catg)
                {
                    if (ci.constant.Equals(constName))
                        return ci.id;
                }
            }

            if (cats != null)
            {
                foreach (CategoryInfo ci in cats)
                {
                    if (ci.constant.Equals(constName))
                        return ci.id;
                }
            }
            throw new Exception("Die Kategorie mit der Konstante " + constName + " existiert nicht in diesem Objekttyp!");
        }

        public CategoryInfo ConstToCategoryInfo(string constName)
        {
            if (catg != null)
            {
                foreach (CategoryInfo ci in catg)
                {
                    if (ci.constant.Equals(constName))
                        return ci;
                }
            }

            if (cats != null)
            {
                foreach (CategoryInfo ci in cats)
                {
                    if (ci.constant.Equals(constName))
                        return ci;
                }
            }

            throw new Exception("Die Kategorie mit der Konstante " + constName + " existiert nicht in diesem Objekttyp!");
        }

        public bool TestForConstant(Category c)
        {
            return TestForConstant(c.Constant);
        }

    }
}
