using System.Collections.Generic;

namespace Dnn.Showcase
{
    public static class Extensions
    {

        // Category Converter
        public static CategoryDTO ToDto(this Community_Showcase_Category item)
        {
            CategoryDTO dto = new CategoryDTO()
            {
                id = item.id,
                name = item.name
            };

            return dto;
        }
        public static Community_Showcase_Category ToItem(this CategoryDTO dto, Community_Showcase_Category item)
        {
            if (item == null)
            {
                item = new Community_Showcase_Category();
            }

            if (dto == null)
            {
                return item;
            }

            item.id = dto.id;
            item.name = dto.name;

            return item;
        }



        // Site Converter
        public static SiteDTO ToDto(this Community_Showcase_Site item)
        {
            SiteDTO dto = new SiteDTO()
            {
                id = item.id,
                name = item.name,
                created_date = item.created_date,
                description = item.description,
                is_active = item.is_active,
                thumbnail = item.thumbnail,
                url = item.url,
                user_id = item.user_id
            };

            dto.site_categories = new List<SiteCategoryDTO>();
            foreach (Community_Showcase_SiteCategory site_category in item.Community_Showcase_SiteCategories)
            {
                SiteCategoryDTO siteCategoryDTO = new SiteCategoryDTO()
                {
                    category_id = site_category.category_id,
                    site_id = site_category.site_id,
                    site_name = site_category.Community_Showcase_Site.name,
                    category_name = site_category.Community_Showcase_Category.name
                };
                dto.site_categories.Add(siteCategoryDTO);
            }


            return dto;
        }
        public static Community_Showcase_Site ToItem(this SiteDTO dto, Community_Showcase_Site item)
        {
            if (item == null)
            {
                item = new Community_Showcase_Site();
            }

            if (dto == null)
            {
                return item;
            }

            item.id = dto.id;
            item.name = dto.name;
            item.created_date = dto.created_date;
            item.description = dto.description;
            item.is_active = dto.is_active;
            item.thumbnail = dto.thumbnail;
            item.url = dto.url;
            item.user_id = item.user_id;

            return item;
        }


        // Site Category Converter
        public static SiteCategoryDTO ToDto(this Community_Showcase_SiteCategory item)
        {
            SiteCategoryDTO dto = new SiteCategoryDTO()
            {
                id = item.id,
                site_id = item.site_id,
                category_id = item.category_id,

                site_name = item.Community_Showcase_Site.name,
                category_name = item.Community_Showcase_Category.name
            };

            return dto;
        }
        public static Community_Showcase_SiteCategory ToItem(this SiteCategoryDTO dto, Community_Showcase_SiteCategory item)
        {
            if (item == null)
            {
                item = new Community_Showcase_SiteCategory();
            }

            if (dto == null)
            {
                return item;
            }

            item.id = dto.id;
            item.site_id = dto.site_id;
            item.category_id = dto.category_id;

            return item;
        }
    }
}