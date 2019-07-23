using KinderJoy.Domain.Infrastructure;
using KinderJoy.Domain.Entities;
using KinderJoy.Domain.Entities.Word;
using KinderJoy.Site.Models.Word;
using KinderJoy.Site.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinderJoy.Site.Infrastructure.Word
{
    public class WordService : KinderJoy.Site.Infrastructure.Word.IWordService
    {
        private AppDbContext db;

        public WordService(AppDbContext db)
        {
            this.db = db;
        }

        public WordEvent EntryWord(WordEntry entry, string ip)
        {
            PersonalInfos personal = new PersonalInfos
            {
                Name = entry.Name,
                Mobile = EventUtil.FormatMobile(entry.Mobile, ""),
                Age = entry.Age,
                Gender = entry.Genger,
                IpAddress = ip,
                EventId = "20150801-Word"
            };

            int St = this.CalcSt();

            WordEvent wordEvent = new WordEvent
            {
                Channel = entry.Channel,
                GiftType = entry.GiftType,
                Ip = ip,
                St = St
            };

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    db.PersonalInfos.Add(personal);
                    db.SaveChanges();
                    wordEvent.PersonalInfoId = personal.PersonalInfoId;
                    db.WordEvents.Add(wordEvent);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("일시적인 오류가 발생 하였습니다. 새로고침 후 다시 시도해 주세요.");
                }
            }
            return wordEvent;
        }

        public void ShareWord(WordShare entry)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    WordEventShare wordShare = new WordEventShare
                    {
                        Channel = entry.Channel,
                        Ip = entry.Ip,
                        PostId = entry.PostId,
                        SnsId = entry.SnsId,
                        SnsType = entry.SnsType,
                        WordEventId = entry.WordEventId
                    };
                    db.WordEventShares.Add(wordShare);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new InvalidOperationException("일시적인 오류가 발생 하였습니다. 새로고침 후 다시 시도해 주세요.");
                }
            }
        }

        public int CalcSt()
        {
            int St = 1;
            DateTime now = DateTime.Now;
            if (now <= new DateTime(2015, 8, 20, 23, 59, 59))
            {
                St = 1;
            }
            else if (now <= new DateTime(2015, 8, 27, 23, 59, 59))
            {
                St = 2;
            }
            else if (now <= new DateTime(2015, 9, 3, 23, 59, 59))
            {
                St = 3;
            }
            else
            {
                St = 4;
            }

            return St;
        }

        public bool IsWinnerResultSt(int St)
        {
            return db.WordWinners.Count(e => e.St == St) > 0;
        }

        public List<WordWinner> GetWordWinners(int St, string GiftType)
        {
            return db.WordWinners.Where(e => e.St == St && e.GiftType == GiftType).OrderByDescending(e => e.Id).ToList();
        }

        public int CalcWinnerLastSt()
        {
            return db.WordWinners.OrderByDescending(e => e.St).FirstOrDefault().St;
        }
    }
}