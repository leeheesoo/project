using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Repository.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Exceptions;
using LinqKit;
using KinderJoy.Domain.Models.MagicKinderAppLaunchingEvent;

namespace KinderJoy.Domain.Service.MagicKinderAppLaunchingEvent
{
    public class MagicKinderAppLaunchingService : IMagicKinderAppLaunchingService
    {
        private IMagicKinderAppLaunchingRepository repository;

        public MagicKinderAppLaunchingService(IMagicKinderAppLaunchingRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// 개인정보 저장
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public MagicKinderAppLaunching CreateMagicKinderAppLaunching(MagicKinderAppLaunching entry)
        {
            var result = repository.Add(entry);
            repository.Save();
            return result;
        }

        /// <summary>
        /// 관리자 리스트
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IQueryable<MagicKinderAppLaunching> GetAdminMagicKinderAppLaunching(MagicKinderAppLaunchingQueryOptions options)
        {
            return repository.GetAll().AsExpandable().Where(options.BuildPredicate());
        }

        /// <summary>
        /// 이벤트 참여 현황
        /// </summary>
        /// <returns></returns>
        public IQueryable<MagicKinderAppLaunching> GetMagicKinderAppLaunching()
        {
            return repository.GetAll().Where(x => x.IsShow).OrderByDescending(x => x.CreateDate);
        }

        /// <summary>
        /// 관리자 통계 리스트
        /// </summary>
        /// <returns></returns>
        public IQueryable<AdminMagicKinderStatsViewModel> GetStatistics()
        {
            var query = repository.GetAll()
                .GroupBy(x => x.Mobile).Select(x => new AdminMagicKinderStatsViewModel
                {
                    Mobile = x.Key,
                    Name = x.Max(e => e.Name),
                    TotalCount = x.Count(),
                    TotalIsShowCount = x.Count(e => e.IsShow),
                    ColoringCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Coloring),
                    ColoringIsShowCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Coloring && e.IsShow),
                    VideoCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Video),
                    VideoIsShowCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Video && e.IsShow),
                    PlayingCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Playing),
                    PlayingIsShowCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Playing && e.IsShow),
                    StoryCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Story),
                    StoryIsShowCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Story && e.IsShow),
                    EarthTripCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.EarthTrip),
                    EarthTripIsShowCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.EarthTrip && e.IsShow),
                    EtcCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Etc),
                    EtcIsShowCount = x.Count(e => e.ScreenShotType == MagicKinderScreenShotTypes.Etc && e.IsShow)
                });

            return query.OrderByDescending(x => x.TotalCount);
        }

        /// <summary>
        /// 노출여부 수정
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public MagicKinderAppLaunching UpdateIsShowMagicKinderAppLaunching(long id)
        {
            var query = repository.SingleOrDefault(x => x.Id == id);

            if (query == null)
            {
                throw new EventServiceException("500", "참여자 정보가 잘못되었습니다.", id);
            }

            query.IsShow = !query.IsShow;


            var result = repository.Update(query);
            repository.Save();

            return result;
        }
    }
}
